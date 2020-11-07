using Microsoft.AspNetCore.Http;
using NhapMonCNPM.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NhapMonCNPM.QuanlyMonAn.DTO
{

    
    public class ThemMonan
    {
        public string TenMonAn { get; set; }
        public double DonGia { get; set; }
        public DonViTinh DonViTinh { get; set; }
        //public IFormFile HinhAnh { get; set; }
        public List<long> DanhsachNguyenlieu { get; set; }
    }
}
