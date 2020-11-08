using Abp.Domain.Entities;
using NhapMonCNPM.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.QuanLyNguyenLieu.Dto
{
    public class DanhSachNguyenLieuPaging:Entity<long>
    {
        public string TenNguyenLieu { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
    }
    public class DanhSachNguyenLieuInput
    {
        public GridParam Param { get; set; }
        public string TenNguyenLieu { get; set; }
    }
}
