using NhapMonCNPM.Entities;
using NhapMonCNPM.Extension;
using NhapMonCNPM.Paging;
using NhapMonCNPM.QuanlyMonAn.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Abp.Extensions;
using Abp.Collections.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace NhapMonCNPM.QuanlyMonAn
{
    public class QuanlyMonanAppService : NhapMonCNPMAppServiceBase
    {

        public async Task<GridResult<GetMonanOutput>> DanhSachMonAn(GetMonanInput input)
        {
            var query = WorkScope.GetRepo<ChiTietMonAn>().GetAllIncluding(s => s.MonAn, k => k.NguyenLieu)
                .WhereIf(!input.tenMonan.IsNullOrWhiteSpace(), s => s.MonAn.TenMonAn.Contains(input.tenMonan, StringComparison.OrdinalIgnoreCase))
                .WhereIf(input.dongia.HasValue, s => s.MonAn.DonGia >= input.dongia)
                .WhereIf(!input.nguyenlieu.IsNullOrWhiteSpace(), s => s.NguyenLieu.TenNguyenLieu.Contains(input.nguyenlieu, StringComparison.OrdinalIgnoreCase))
                .GroupBy(s => s.MonAn.Id)
                .Select(s => new GetMonanOutput
                {
                    DanhsachNguyenLieu = s.Select(k => k.NguyenLieu.TenNguyenLieu),
                    GiaMon = s.FirstOrDefault().MonAn.DonGia,
                    HinhAnh = s.FirstOrDefault().MonAn.HinhAnh,
                    id = s.Key,
                    tenMonan = s.FirstOrDefault().MonAn.TenMonAn,
                }).AsQueryable();
            return query.GetGridResultSync(query, input.gridParam);
        }


        public async Task ThemMonAn(ThemMonan input)
        {
            var resual = new MonAn
            {
                TenMonAn = input.TenMonAn,
                DonGia = input.DonGia,
                DonViTinh = input.DonViTinh,
            };
            var id = await WorkScope.InsertAndGetIdAsync(resual);
            var chitietMonan = new List<ChiTietMonAn>();
            foreach (var i in input.DanhsachNguyenlieu)
            {
                var chitiet = new ChiTietMonAn
                {
                    MaMonAn = id,
                    MaNguyenLieu = i,
                };
                chitietMonan.Add(chitiet);
            }
            await WorkScope.InsertRangeAsync(chitietMonan);


        }

        public async Task ThemNguyenLieu(ThemNguyenLieu input)
        {
            var mapped = ObjectMapper.Map<NguyenLieu>(input);
            await WorkScope.InsertAsync(mapped);

        }

        public async Task SuaMonAn(SuaMonAn input)
        {
            var MonAn = await WorkScope.GetAll<MonAn>().Where(s => s.Id == input.id).FirstOrDefaultAsync();
            MonAn.TenMonAn = input.TenMonAn;
            MonAn.DonGia = input.DonGia;
            MonAn.DonViTinh = input.DonViTinh;

            await WorkScope.UpdateAsync(MonAn);

            var ChitietMonAnOldData = await WorkScope.GetAll<ChiTietMonAn>().Where(s => s.MaMonAn == input.id)
                .ToListAsync();
            var ChitietMonAn = ChitietMonAnOldData.Select(s => s.MaNguyenLieu).ToList();

            //ChitietMonAn = ChitietMonAn.FindAll();
            var ThemMoi = input.DanhsachNguyenlieu.Except(ChitietMonAn);
            var XoaDi = ChitietMonAn.Except(input.DanhsachNguyenlieu);

            foreach (var i in ChitietMonAnOldData)
            {
                if (XoaDi.Contains(i.MaNguyenLieu))
                {
                    await WorkScope.DeleteAsync(i);
                }
            }
            var chitietMonan = new List<ChiTietMonAn>();
            foreach (var i in ThemMoi)
            {
                var chitiet = new ChiTietMonAn
                {
                    MaMonAn = input.id,
                    MaNguyenLieu = i,
                };
                chitietMonan.Add(chitiet);
            }
            await WorkScope.InsertRangeAsync(chitietMonan);
        }

        public async Task XoaMonAn(SuaMonAn input)
        {
            var MonAn = await WorkScope.GetAll<MonAn>().Where(s => s.Id == input.id).FirstOrDefaultAsync();

            var ChitietMonAnOldData = await WorkScope.GetAll<ChiTietMonAn>().Where(s => s.MaMonAn == input.id)
                .ToListAsync();
         
            foreach (var i in ChitietMonAnOldData)
            {
                await WorkScope.DeleteAsync(i);
            }

            await WorkScope.DeleteAsync(MonAn);
        }
    }
}
