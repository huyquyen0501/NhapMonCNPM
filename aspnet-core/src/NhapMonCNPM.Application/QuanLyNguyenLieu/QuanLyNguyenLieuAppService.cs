using Microsoft.AspNetCore.Mvc;
using NhapMonCNPM.QuanLyNguyenLieu.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NhapMonCNPM.Entities;
using Microsoft.EntityFrameworkCore;
using NhapMonCNPM.Paging;
using NhapMonCNPM.QuanlyMonAn.DTO;
using Abp.Collections.Extensions;
using Abp.Extensions;
using NhapMonCNPM.Extension;

namespace NhapMonCNPM.QuanLyNguyenLieu
{
    public class QuanLyNguyenLieuAppService:NhapMonCNPMAppServiceBase
    {
        [HttpGet]
        public async Task<List<NameAndId>> DanhSachNguyenLieuNoPaging()
        {
            return await WorkScope.GetAll<NguyenLieu>().Select(s => new NameAndId { Id = s.Id, TenNguyenLieu = s.TenNguyenLieu }).ToListAsync();
        }
        public async Task ThemNguyenLieu(ThemNguyenLieu input)
        {
            var mapped = ObjectMapper.Map<NguyenLieu>(input);
            await WorkScope.InsertAsync(mapped);

        }
        public async Task<GridResult<DanhSachNguyenLieuPaging>> DanhSachNguyenLieuPaging(DanhSachNguyenLieuInput input)
        {

            var query = WorkScope.GetAll<NguyenLieu>().WhereIf(!input.TenNguyenLieu.IsNullOrWhiteSpace(), s => s.TenNguyenLieu.Contains(input.TenNguyenLieu, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(s=>s.LastModificationTime)
                .Select(s => new DanhSachNguyenLieuPaging
                {
                    Id = s.Id,
                    SoLuong = s.SoLuong,
                    DonGia = s.DonGia,
                    TenNguyenLieu = s.TenNguyenLieu
                }).AsQueryable();
            return query.GetGridResultSync(query, input.Param);
        }
    }
}
