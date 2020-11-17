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
using Abp.UI;

namespace NhapMonCNPM.QuanlyMonAn
{
    public class QuanlyMonanAppService : NhapMonCNPMAppServiceBase
    {

        public async Task<GridResult<GetMonanOutput>> DanhSachMonAn(GetMonanInput input)
        {

            var query = (from s in WorkScope.GetAll<MonAn>()
                .WhereIf(!input.tenMonan.IsNullOrWhiteSpace(), s => s.TenMonAn.Contains(input.tenMonan, StringComparison.OrdinalIgnoreCase))
                .WhereIf(input.dongia.HasValue, s => s.DonGia >= input.dongia)
                        join k in WorkScope.GetRepo<ChiTietMonAn>().GetAllIncluding(s=>s.NguyenLieu)
                        on s.Id equals k.MaMonAn into temped
                        from i in temped.DefaultIfEmpty()
                        select new GetMonanOutput
                        {
                            DanhsachNguyenLieu = temped.Select(k => new DanhSachNguyenLieu { SoLuong = k.SoLuong, MaNguyenLieu = k.MaNguyenLieu, TenNguyenLieu = k.NguyenLieu.TenNguyenLieu }),
                            GiaMon = s.DonGia,
                            HinhAnh = s.HinhAnh,
                            id = s.Id,
                            tenMonan = s.TenMonAn,
                            DonViTinh = s.DonViTinh
                        })
                        .WhereIf(!input.nguyenlieu.IsNullOrEmpty(),s=>(s.DanhsachNguyenLieu.Select(k=>k.TenNguyenLieu)).Contains(input.nguyenlieu)).AsEnumerable()
                        .GroupBy(s=>s.id).Select(s=> new GetMonanOutput
                        {
                            DanhsachNguyenLieu=s.FirstOrDefault().DanhsachNguyenLieu,
                            DonViTinh=s.FirstOrDefault().DonViTinh,
                            GiaMon=s.FirstOrDefault().GiaMon,
                            HinhAnh=s.FirstOrDefault().HinhAnh,
                            id=s.Key,
                            tenMonan=s.FirstOrDefault().tenMonan
                        })
                        .AsQueryable();
            return query.GetGridResultSync(query, input.gridParam);
        }
        [HttpGet]
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
            if(input.DonGia <= 0|| !input.DonGia.HasValue)
            {
                throw new UserFriendlyException("Giá món ăn không được nhỏ hơn hoặc bằng 0");
            }
            if (input.TenMonAn.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("Tên món ăn không được để trống");
            }
            var resual = new MonAn
            {
                TenMonAn = input.TenMonAn,
                DonGia = input.DonGia??0,
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
            if (input.DonGia <= 0)
            {
                throw new UserFriendlyException("Giá món ăn không được nhỏ hơn =0");
            }
            if (input.TenMonAn.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("Tên món ăn không được để trống");
            }
            var MonAn = await WorkScope.GetAll<MonAn>().Where(s => s.Id == input.id).FirstOrDefaultAsync();
            MonAn.TenMonAn = input.TenMonAn;
            MonAn.DonGia = input.DonGia??0;
            MonAn.DonViTinh = input.DonViTinh;
            string fileLocation = UploadFile.CreateFolderIfNotExists(ConstantVarible.wwwRootFolder, "Images");
            string fileName = await UploadFile.UploadAsync(fileLocation, input.HinhAnh);
            MonAn.HinhAnh = $"{ConstantVarible.RootUrl}Images/{fileName}";
            await WorkScope.UpdateAsync(MonAn);

            
        }
        [HttpPost]
        public async Task CapNhatNguyenLieu(CapNhatNguyenLieuDto input)
        {
            var ChitietMonAnOldData = await WorkScope.GetAll<ChiTietMonAn>().Where(s => s.MaMonAn == input.IdMonAn)
                .ToListAsync();
            var ChitietMonAn = ChitietMonAnOldData.Select(s => s.MaNguyenLieu).ToList();
            var danhsachIdInput = input.DanhSachNguyenLieu.Where(s=>s.SoLuong>0).Select(s => s.MaNguyenLieu).ToList();
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
                foreach (var j in input.DanhSachNguyenLieu)
                {
                    {
                        if (i == j.MaNguyenLieu)
                        {
                            var chitiet = new ChiTietMonAn
                            {
                                MaMonAn = input.IdMonAn,
                                MaNguyenLieu = i,
                                SoLuong = j.SoLuong
                            };
                            chitietMonanThemVao.Add(chitiet);
                        }
                    }
                }
            await WorkScope.InsertRangeAsync(chitietMonanThemVao);
            //Sua nguyen lieu
            var danhSachSua = ChitietMonAn.Except(XoaDi);
            var DanhSachInputLaSua = input.DanhSachNguyenLieu.Where(s => danhSachSua.Contains(s.MaNguyenLieu)).ToList();
            foreach (var i in DanhSachInputLaSua)
            {
                foreach (var j in ChitietMonAnOldData)
                {
                    if (i.MaNguyenLieu == j.MaNguyenLieu)
                    {
                        j.SoLuong = i.SoLuong;
                    }
                }
            }
            await WorkScope.UpdateRangeAsync(ChitietMonAnOldData);
        }
    }
}
