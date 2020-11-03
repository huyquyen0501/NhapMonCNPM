using Abp.AutoMapper;
using NhapMonCNPM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.QuanlyMonAn.DTO
{
    [AutoMapTo(typeof(NguyenLieu))]
    public class ThemNguyenLieu
    {
        public string TenNguyenLieu { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
    }
}
