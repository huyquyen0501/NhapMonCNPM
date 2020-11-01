using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class ChiTietMonAn:FullAuditedEntity<long>
    {
        
        public long MaMonAn { get; set; }
        [ForeignKey(nameof(MaMonAn))]
        public MonAn MonAn { get; set; }
        public long MaNguyenLieu { get; set; }
        [ForeignKey(nameof(MaNguyenLieu))]
        public NguyenLieu NguyenLieu { get; set; }
        public double SoLuong { get; set; }
    }
}
