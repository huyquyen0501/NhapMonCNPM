using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class KhachHang:Entity<long>
    {
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
    }
}
