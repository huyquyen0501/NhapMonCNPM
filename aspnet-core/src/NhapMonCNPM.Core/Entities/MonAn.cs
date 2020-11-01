using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class MonAn:FullAuditedEntity<long>
    {
        public string TenMonAn { get; set; }
        public double DonGia { get; set; }
        public DonViTinh DonViTinh { get; set; }
        public string HinhAnh { get; set; }
    }
    public enum DonViTinh { 
        Dia =1,
        Bat = 2,
        Con = 3,
        Kg=4
    };
}
