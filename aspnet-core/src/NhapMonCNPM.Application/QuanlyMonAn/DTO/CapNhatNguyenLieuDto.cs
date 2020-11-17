using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.QuanlyMonAn.DTO
{
    public class CapNhatNguyenLieuDto
    {
        public long IdMonAn { get; set; }
        public List<DanhSachNguyenLieu> DanhSachNguyenLieu { get; set; }
    }
}
