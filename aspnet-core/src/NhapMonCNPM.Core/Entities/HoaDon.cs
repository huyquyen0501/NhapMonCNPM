using Abp.Domain.Entities.Auditing;
using NhapMonCNPM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class HoaDon:FullAuditedEntity<long>
    {
        public DateTime? ThoiGianKhachThanhToan { get; set; }
        public long MaKhachHang  { get; set; }
        [ForeignKey(nameof(MaKhachHang))]
        public KhachHang KhachHang { get; set; }
        public double? SoTienKhachThanhToan { get; set; }
        public long? MaThuNgan { get; set; }
        [ForeignKey(nameof(MaThuNgan))]
        public User ThuNgan { get; set; }
    }
}
