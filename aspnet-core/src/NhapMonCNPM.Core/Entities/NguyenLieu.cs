using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class NguyenLieu:FullAuditedEntity<long>
    {
        public string TenNguyenLieu { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
    }
}
