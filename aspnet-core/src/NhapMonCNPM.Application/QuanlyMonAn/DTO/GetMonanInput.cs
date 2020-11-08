using NhapMonCNPM.Entities;
using NhapMonCNPM.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace NhapMonCNPM.QuanlyMonAn.DTO
{
    public class GetMonanInput
    {
        public GridParam gridParam { get; set; }
        public string tenMonan { get; set; }
        public double? dongia { get; set; }
        public string nguyenlieu { get; set; }
    }

    public class GetMonanOutput
    {
        public long id { get; set; }
        public string tenMonan { get; set; }
        public double GiaMon { get; set; }
        public IEnumerable<DanhSachNguyenLieu> DanhsachNguyenLieu { get; set; }
        public DonViTinh DonViTinh { get; set; }
        public string HinhAnh { get; set; }
    }
    public class DanhSachNguyenLieu {
        public long MaNguyenLieu { get; set; }
        public string TenNguyenLieu { get; set; }
        public double SoLuong { get; set; }
    }
}
