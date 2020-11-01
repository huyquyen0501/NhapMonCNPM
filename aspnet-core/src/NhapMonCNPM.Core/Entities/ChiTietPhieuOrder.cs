using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class ChiTietPhieuOrder:Entity<long>
    {
        public long MaPhieuOrder { get; set; }
        public PhieuOrder PhieuOrder { get; set; }
        public long MaMonAn { get; set; }
        public MonAn MonAn { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
    }
}
