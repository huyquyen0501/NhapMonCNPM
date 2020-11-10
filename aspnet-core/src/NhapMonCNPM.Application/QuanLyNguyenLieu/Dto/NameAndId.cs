using NhapMonCNPM.QuanlyMonAn.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.QuanLyNguyenLieu.Dto
{
    public class NameAndId: IdNguyenLieuVaSoLuong
    {
        public long Id { get; set; }
        public string TenNguyenLieu { get; set; }
    }
}
