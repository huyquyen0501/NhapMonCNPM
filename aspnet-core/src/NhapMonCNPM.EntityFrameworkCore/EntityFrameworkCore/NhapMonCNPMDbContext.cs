using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using NhapMonCNPM.Authorization.Roles;
using NhapMonCNPM.Authorization.Users;
using NhapMonCNPM.MultiTenancy;
using NhapMonCNPM.Entities;

namespace NhapMonCNPM.EntityFrameworkCore
{
    public class NhapMonCNPMDbContext : AbpZeroDbContext<Tenant, Role, User, NhapMonCNPMDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<ChiTietMonAn> ChiTietMonAns { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuOrder> ChiTietPhieuOrders { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<NguyenLieu> NguyenLieus { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<PhieuOrder> PhieuOrders { get; set; }

        public NhapMonCNPMDbContext(DbContextOptions<NhapMonCNPMDbContext> options)
            : base(options)
        {
        }
    }
}
