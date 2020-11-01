using Abp.Domain.Entities.Auditing;
using NhapMonCNPM.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class PhieuNhap:FullAuditedEntity<long>
    {
        [ForeignKey(nameof(CreatorUserId))]
        public User NguoiYeuCau { get; set; }
        public long? MaQuanLy { get; set; }
        [ForeignKey(nameof(MaQuanLy))]
        public User QuanLy { get; set; }
        public string LyDoNhap { get; set; }
        public DateTime? NgayNhap { get; set; }
        public TrangThaiPhieuNhap TrangThaiPhieuNhap  { get; set; }
    }
public enum TrangThaiPhieuNhap
{
    DangChoDuyet = 0,
    DongY=1,
    TuChoi = -1
};
}
