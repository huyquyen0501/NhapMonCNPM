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
using NhapMonCNPM.Helper.MyUploadFile;
using NhapMonCNPM.Constants;

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
                    DanhsachNguyenLieu = s.Select(k=> new DanhSachNguyenLieu { SoLuong=k.SoLuong,MaNguyenLieu=k.MaNguyenLieu,TenNguyenLieu=k.NguyenLieu.TenNguyenLieu}),
                    GiaMon = s.FirstOrDefault().MonAn.DonGia,
                    HinhAnh = s.FirstOrDefault().MonAn.HinhAnh,
                    id = s.Key,
                    tenMonan = s.FirstOrDefault().MonAn.TenMonAn,
                    DonViTinh=s.FirstOrDefault().MonAn.DonViTinh
                }).AsQueryable();
            return query.GetGridResultSync(query, input.gridParam);
        }
        public async Task<GetMonanOutput> ChiTietMonAn(long Id)
        {
            return await WorkScope.GetRepo<ChiTietMonAn>().GetAllIncluding(s => s.MonAn, k => k.NguyenLieu)
               .Where(s=>s.MaMonAn==Id)
                .GroupBy(s => s.MonAn.Id)
                .Select(s => new GetMonanOutput
                {
                    DanhsachNguyenLieu = s.Select(k => new DanhSachNguyenLieu { SoLuong = k.SoLuong, MaNguyenLieu = k.MaNguyenLieu, TenNguyenLieu = k.NguyenLieu.TenNguyenLieu }),
                    GiaMon = s.FirstOrDefault().MonAn.DonGia,
                    HinhAnh = s.FirstOrDefault().MonAn.HinhAnh,
                    id = s.Key,
                    tenMonan = s.FirstOrDefault().MonAn.TenMonAn,
                    DonViTinh = s.FirstOrDefault().MonAn.DonViTinh
                }).FirstOrDefaultAsync();
        }

        public async Task ThemMonAn([FromForm] ThemMonan input)
        {
            var resual = new MonAn
            {
                TenMonAn = input.TenMonAn,
                DonGia = input.DonGia,
                DonViTinh = input.DonViTinh,
            };

            string fileLocation = UploadFile.CreateFolderIfNotExists(ConstantVarible.wwwRootFolder, "Images");

            string fileName = await UploadFile.UploadAsync(fileLocation, input.HinhAnh);
            resual.HinhAnh = $"{ConstantVarible.RootUrl}Images/{fileName}";
            var id = await WorkScope.InsertAndGetIdAsync(resual);
            var chitietMonan = new List<ChiTietMonAn>();
            foreach (var i in input.DanhsachNguyenlieu)
            {
                var chitiet = new ChiTietMonAn
                {
                    MaMonAn = id,
                    MaNguyenLieu = i.IdNguyenLieu,
                    SoLuong = i.SoLuong,
                };
                chitietMonan.Add(chitiet);
            }
            await WorkScope.InsertRangeAsync(chitietMonan);


        }



        public async Task SuaMonAn([FromForm] SuaMonAn input)
        {
            var MonAn = await WorkScope.GetAll<MonAn>().Where(s => s.Id == input.id).FirstOrDefaultAsync();
            MonAn.TenMonAn = input.TenMonAn;
            MonAn.DonGia = input.DonGia;
            MonAn.DonViTinh = input.DonViTinh;
            string fileLocation = UploadFile.CreateFolderIfNotExists(ConstantVarible.wwwRootFolder, "Images");
            string fileName = await UploadFile.UploadAsync(fileLocation, input.HinhAnh);
            MonAn.HinhAnh = $"{ConstantVarible.RootUrl}Images/{fileName}";
            await WorkScope.UpdateAsync(MonAn);

            var ChitietMonAnOldData = await WorkScope.GetAll<ChiTietMonAn>().Where(s => s.MaMonAn == input.id)
                .ToListAsync();
            var ChitietMonAn = ChitietMonAnOldData.Select(s => s.MaNguyenLieu).ToList();
            var danhsachIdInput = input.DanhsachNguyenlieu.Select(s => s.IdNguyenLieu).ToList();
            //ChitietMonAn = ChitietMonAn.FindAll();
            var ThemMoi = danhsachIdInput.Except(ChitietMonAn);
            var XoaDi = ChitietMonAn.Except(danhsachIdInput);
            //Sua nguyen lieu
            

            foreach (var i in ChitietMonAnOldData)
            {
                if (XoaDi.Contains(i.MaNguyenLieu))
                {
                    await WorkScope.DeleteAsync(i);
                }
            }
            var chitietMonanThemVao = new List<ChiTietMonAn>();
            foreach (var i in ThemMoi)
                foreach (var j in input.DanhsachNguyenlieu)
                {
                    {
                        if (i == j.IdNguyenLieu)
                        {
                            var chitiet = new ChiTietMonAn
                            {
                                MaMonAn = input.id,
                                MaNguyenLieu = i,
                                SoLuong =j.SoLuong
                            };
                            chitietMonanThemVao.Add(chitiet);
                        }
                    }
                }
            await WorkScope.InsertRangeAsync(chitietMonanThemVao);
            //Sua nguyen lieu
            var danhSachSua = ChitietMonAn.Except(XoaDi);
            var DanhSachInputLaSua = input.DanhsachNguyenlieu.Where(s => danhSachSua.Contains(s.IdNguyenLieu)).ToList();
            foreach(var i in DanhSachInputLaSua)
            {
                foreach(var j in ChitietMonAnOldData)
                {
                    if(i.IdNguyenLieu == j.MaNguyenLieu)
                    {
                        j.SoLuong = i.SoLuong;
                    }
                }
            }
            await WorkScope.UpdateRangeAsync(ChitietMonAnOldData);
        }
    }
}
