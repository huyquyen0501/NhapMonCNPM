using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NhapMonCNPM.Entities
{
    public class ChiTietPhieuNhap:FullAuditedEntity<long>
    {
        public long MaPHieuNhap { get; set; }
        [ForeignKey(nameof(MaPHieuNhap))]
        public PhieuNhap PhieuNhap { get; set; }
        public long MaNguyenLieu { get; set; }
        [ForeignKey(nameof(MaNguyenLieu))]
        public NguyenLieu NguyenLieu { get; set; }
        public double DonGia { get; set; }
        public double SoLuong { get; set; }
    }
}
