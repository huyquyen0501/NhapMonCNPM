using Abp.Domain.Entities.Auditing;
using NhapMonCNPM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class PhieuOrder:FullAuditedEntity<long>
    {
        [ForeignKey(nameof(CreatorUserId))]
        public User NguoiTaoPhieu { get; set; }
        public long MaHoaDon { get; set; }
        [ForeignKey(nameof(MaHoaDon))]
        public HoaDon HoaDon { get; set; }
    }
}
