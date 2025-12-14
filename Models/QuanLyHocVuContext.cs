using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyHocVu.Models;

public partial class QuanLyHocVuContext : DbContext
{
    public QuanLyHocVuContext()
    {
    }

    public QuanLyHocVuContext(DbContextOptions<QuanLyHocVuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CanBo> CanBos { get; set; }

    public virtual DbSet<ChuongTrinhDaoTao> ChuongTrinhDaoTaos { get; set; }

    public virtual DbSet<ChiTietChuongTrinhDaoTao> ChiTietChuongTrinhDaoTaos { get; set; }

    public virtual DbSet<DangKyHocPhan> DangKyHocPhans { get; set; }

    public virtual DbSet<DiemHocPhan> DiemHocPhans { get; set; }

    public virtual DbSet<DiemCongTacXaHoi> DiemCongTacXaHois { get; set; }

    public virtual DbSet<DiemRenLuyen> DiemRenLuyens { get; set; }

    public virtual DbSet<GiangVien> GiangViens { get; set; }

    public virtual DbSet<HoatDongCtxh> HoatDongCtxhs { get; set; }

    public virtual DbSet<HocKy> HocKies { get; set; }

    public virtual DbSet<HocPhi> HocPhis { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<KhoaNganh> KhoaNganhs { get; set; }

    public virtual DbSet<LopHocPhan> LopHocPhans { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<Nganh> Nganhs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhongHoc> PhongHocs { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    public virtual DbSet<SinhVienHoatDongCtxh> SinhVienHoatDongCtxhs { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=QuanLyHocVu;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CanBo>(entity =>
        {
            entity.HasBaseType<NguoiDung>();
            entity.ToTable("CanBo");

            entity.Property(e => e.TinhTrangCongTac).HasMaxLength(50);
        });

        modelBuilder.Entity<ChuongTrinhDaoTao>(entity =>
        {
            entity.HasKey(e => e.MaCtdt).HasName("PK__ChuongTr__1E4E40E44E000967");

            entity.ToTable("ChuongTrinhDaoTao");

            entity.HasIndex(e => e.MaNganh, "UQ__ChuongTr__A2CEF50C3B68DCA1").IsUnique();

            entity.Property(e => e.MaCtdt)
                .HasMaxLength(10)
                .HasColumnName("MaCTDT");
            entity.Property(e => e.MaNganh).HasMaxLength(10);
            entity.Property(e => e.TenCtdt)
                .HasMaxLength(100)
                .HasColumnName("TenCTDT");

            entity.HasOne(d => d.MaNganhNavigation).WithOne(p => p.ChuongTrinhDaoTao)
                .HasForeignKey<ChuongTrinhDaoTao>(d => d.MaNganh)
                .HasConstraintName("FK__ChuongTri__MaNga__403A8C7D");
        });

        modelBuilder.Entity<ChiTietChuongTrinhDaoTao>(entity =>
        {
            entity.HasKey(e => new { e.MaCtdt, e.MaMonHoc });

            entity.ToTable("ChiTietChuongTrinh");

            entity.Property(e => e.MaCtdt).HasMaxLength(10).HasColumnName("MaCTDT");
            entity.Property(e => e.MaMonHoc).HasMaxLength(10);

            entity.HasOne(d => d.MaCtdtNavigation).WithMany(p => p.ChiTietChuongTrinhDaoTaos)
                .HasForeignKey(d => d.MaCtdt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietChuongTrinhDaoTao_ChuongTrinhDaoTao");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.ChiTietChuongTrinhDaoTaos)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietChuongTrinhDaoTao_MonHoc");
        });

        modelBuilder.Entity<DangKyHocPhan>(entity =>
        {
            entity.HasKey(e => new { e.MaSinhVien, e.MaLopHocPhan }).HasName("PK__DangKyHo__1BBF66B85BAAA26B");

            entity.ToTable("DangKyHocPhan");

            entity.Property(e => e.MaSinhVien).HasMaxLength(10);
            entity.Property(e => e.MaLopHocPhan).HasMaxLength(10);
            entity.Property(e => e.TrangThai).HasMaxLength(20);

            entity.HasOne(d => d.MaLopHocPhanNavigation).WithMany(p => p.DangKyHocPhans)
                .HasForeignKey(d => d.MaLopHocPhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DangKyHoc__MaLop__628FA481");

            entity.HasOne(d => d.MaSinhVienNavigation).WithMany(p => p.DangKyHocPhans)
                .HasForeignKey(d => d.MaSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DangKyHoc__MaSin__619B8048");
        });

        modelBuilder.Entity<DiemHocPhan>(entity =>
        {
            entity.HasKey(e => new { e.MaSinhVien, e.MaLopHocPhan });

            entity.ToTable("DiemHocPhan");

            entity.Property(e => e.MaSinhVien).HasMaxLength(10);
            entity.Property(e => e.MaLopHocPhan).HasMaxLength(10);

            entity.HasOne(d => d.MaLopHocPhanNavigation).WithMany(p => p.DiemHocPhans)
                .HasForeignKey(d => d.MaLopHocPhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiemHocPhan_LopHocPhan");

            entity.HasOne(d => d.MaSinhVienNavigation).WithMany(p => p.DiemHocPhans)
                .HasForeignKey(d => d.MaSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiemHocPhan_SinhVien");
        });

        modelBuilder.Entity<DiemCongTacXaHoi>(entity =>
        {
            entity.HasKey(e => e.MaSinhVien).HasName("PK__DiemCong__939AE775E1723318");

            entity.ToTable("DiemCongTacXaHoi");

            entity.Property(e => e.MaSinhVien).HasMaxLength(10);
            entity.Property(e => e.GhiChu).HasMaxLength(200);

            entity.HasOne(d => d.MaSinhVienNavigation).WithOne(p => p.DiemCongTacXaHoi)
                .HasForeignKey<DiemCongTacXaHoi>(d => d.MaSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiemCongT__MaSin__797309D9");
        });

        modelBuilder.Entity<DiemRenLuyen>(entity =>
        {
            entity.HasKey(e => new { e.MaSinhVien, e.MaHocKy }).HasName("PK__DiemRenL__8271B264F00AD658");

            entity.ToTable("DiemRenLuyen");

            entity.Property(e => e.MaSinhVien).HasMaxLength(10);
            entity.Property(e => e.MaHocKy).HasMaxLength(10);
            entity.Property(e => e.GhiChu).HasMaxLength(200);
            entity.Property(e => e.XepLoai).HasMaxLength(20);

            entity.HasOne(d => d.MaHocKyNavigation).WithMany(p => p.DiemRenLuyens)
                .HasForeignKey(d => d.MaHocKy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiemRenLu__MaHoc__70DDC3D8");

            entity.HasOne(d => d.MaSinhVienNavigation).WithMany(p => p.DiemRenLuyens)
                .HasForeignKey(d => d.MaSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiemRenLu__MaSin__6FE99F9F");
        });

        modelBuilder.Entity<GiangVien>(entity =>
        {
            entity.HasBaseType<NguoiDung>();
            entity.ToTable("GiangVien");

            entity.Property(e => e.MaNguoiDung).HasColumnName("MaGiangVien");

            entity.Property(e => e.ChuyenMon).HasMaxLength(100);
            entity.Property(e => e.MaKhoa).HasMaxLength(10);
            entity.Property(e => e.TinhTrangCongTac).HasMaxLength(50);

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.GiangViens)
                .HasForeignKey(d => d.MaKhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GiangVien__MaKho__4CA06362");
        });

        modelBuilder.Entity<HoatDongCtxh>(entity =>
        {
            entity.HasKey(e => e.MaHoatDong).HasName("PK__HoatDong__BD808BE705646F0D");

            entity.ToTable("HoatDongCTXH");

            entity.Property(e => e.MaHoatDong).HasMaxLength(10);
            entity.Property(e => e.GhiChu).HasMaxLength(200);
            entity.Property(e => e.TenHoatDong).HasMaxLength(200);
        });

        modelBuilder.Entity<HocKy>(entity =>
        {
            entity.HasKey(e => e.MaHocKy).HasName("PK__HocKy__1EB55110DB90B806");

            entity.ToTable("HocKy");

            entity.HasIndex(e => new { e.NamHoc, e.HocKySo }, "UQ_HocKy").IsUnique();

            entity.Property(e => e.MaHocKy).HasMaxLength(10);
            entity.Property(e => e.NamHoc).HasMaxLength(20);
        });

        modelBuilder.Entity<HocPhi>(entity =>
        {
            entity.HasKey(e => e.MaHocPhi).HasName("PK__HocPhi__929232A2C5A48D72");

            entity.ToTable("HocPhi");

            entity.Property(e => e.MaHocPhi).HasMaxLength(10);
            entity.Property(e => e.GiaTheoTin).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("PK__Khoa__6539040518C9B122");

            entity.ToTable("Khoa");

            entity.Property(e => e.MaKhoa).HasMaxLength(10);
            entity.Property(e => e.TenKhoa).HasMaxLength(100);

            entity.HasMany(d => d.KhoaNganhs).WithOne(p => p.Khoa)
                .HasForeignKey(d => d.MaKhoa)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_KhoaNganh_Khoa");
        });

        modelBuilder.Entity<LopHocPhan>(entity =>
        {
            entity.HasKey(e => e.MaLopHocPhan).HasName("PK__LopHocPh__82581CD9760E06F3");

            entity.ToTable("LopHocPhan");

            entity.Property(e => e.MaLopHocPhan).HasMaxLength(10);
            entity.Property(e => e.MaGiangVien).HasMaxLength(10);
            entity.Property(e => e.MaHocKy).HasMaxLength(10);
            entity.Property(e => e.MaMonHoc).HasMaxLength(10);
            entity.Property(e => e.PhongHoc).HasMaxLength(10);

            entity.HasOne(d => d.MaGiangVienNavigation).WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.MaGiangVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHocPha__MaGia__5DCAEF64");

            entity.HasOne(d => d.MaHocKyNavigation).WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.MaHocKy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHocPha__MaHoc__5EBF139D");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHocPha__MaMon__5CD6CB2B");

            entity.HasOne(d => d.PhongHocNavigation).WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.PhongHoc)
                .HasConstraintName("FK__LopHocPha__Phong__7E37BEF6");
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.MaMonHoc).HasName("PK__MonHoc__4127737F4C7C7279");

            entity.ToTable("MonHoc");

            entity.Property(e => e.MaMonHoc).HasMaxLength(10);
            entity.Property(e => e.LoaiMon).HasMaxLength(50);
            entity.Property(e => e.MaHocPhi).HasMaxLength(10);
            entity.Property(e => e.TenMonHoc).HasMaxLength(100);

            entity.HasOne(d => d.MaHocPhiNavigation).WithMany(p => p.MonHocs)
                .HasForeignKey(d => d.MaHocPhi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MonHoc__MaHocPhi__44FF419A");
        });

        modelBuilder.Entity<Nganh>(entity =>
        {
            entity.HasKey(e => e.MaNganh).HasName("PK__Nganh__A2CEF50D0800C777");

            entity.ToTable("Nganh");

            entity.Property(e => e.MaNganh).HasMaxLength(10);
            entity.Property(e => e.TenNganh).HasMaxLength(100);

            entity.HasMany(d => d.KhoaNganhs).WithOne(p => p.Nganh)
                .HasForeignKey(d => d.MaNganh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhoaNganh__MaNga__571DF1D5");
        });

        modelBuilder.Entity<KhoaNganh>(entity =>
        {
            entity.HasKey(e => new { e.MaKhoa, e.MaNganh });

            entity.ToTable("KhoaNganh");

            entity.Property(e => e.MaKhoa).HasMaxLength(10);
            entity.Property(e => e.MaNganh).HasMaxLength(10);

            entity.HasOne(e => e.Khoa)
                .WithMany(k => k.KhoaNganhs)
                .HasForeignKey(e => e.MaKhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KhoaNganh_Khoa");

            entity.HasOne(e => e.Nganh)
                .WithMany(n => n.KhoaNganhs)
                .HasForeignKey(e => e.MaNganh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KhoaNganh_Nganh");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__NguoiDun__C539D76269338DAE");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Cccd, "UQ__NguoiDun__A955A0AA6815457B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D105340F98FDDF").IsUnique();

            entity.Property(e => e.MaNguoiDung).HasMaxLength(10);
            entity.Property(e => e.Cccd)
                .HasMaxLength(20)
                .HasColumnName("CCCD");
            entity.Property(e => e.DiaChiTamTru).HasMaxLength(200);
            entity.Property(e => e.DiaChiThuongTru).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.QueQuan).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
        });

        modelBuilder.Entity<PhongHoc>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__PhongHoc__20BD5E5BD4EAFBF3");

            entity.ToTable("PhongHoc");

            entity.Property(e => e.MaPhong).HasMaxLength(10);
            entity.Property(e => e.Khu).HasMaxLength(50);
            entity.Property(e => e.LoaiPhong).HasMaxLength(50);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasBaseType<NguoiDung>();
            entity.ToTable("SinhVien");

            entity.Property(e => e.MaNguoiDung).HasColumnName("MaNguoiDung");

            entity.Property(e => e.MaNganh).HasMaxLength(10);
            entity.Property(e => e.NienKhoa).HasMaxLength(20);
            entity.Property(e => e.TinhTrangHoc).HasMaxLength(50);

            entity.HasOne(d => d.MaNganhNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.MaNganh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SinhVien__MaNgan__48CFD27E");
        });

        modelBuilder.Entity<SinhVienHoatDongCtxh>(entity =>
        {
            entity.HasKey(e => new { e.MaSinhVien, e.MaHoatDong }).HasName("PK__SinhVien__F842EFCBFCAD311B");

            entity.ToTable("SinhVien_HoatDongCTXH");

            entity.Property(e => e.MaSinhVien).HasMaxLength(10);
            entity.Property(e => e.MaHoatDong).HasMaxLength(10);
            entity.Property(e => e.GhiChu).HasMaxLength(200);

            entity.HasOne(d => d.MaHoatDongNavigation).WithMany(p => p.SinhVienHoatDongCtxhs)
                .HasForeignKey(d => d.MaHoatDong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SinhVien___MaHoa__76969D2E");

            entity.HasOne(d => d.MaSinhVienNavigation).WithMany(p => p.SinhVienHoatDongCtxhs)
                .HasForeignKey(d => d.MaSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SinhVien___MaSin__75A278F5");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__TaiKhoan__C539D76297452ADA");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TaiKhoan__55F68FC0E598A387").IsUnique();

            entity.Property(e => e.MaNguoiDung).HasMaxLength(10);
            entity.Property(e => e.MatKhau).HasMaxLength(200);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasMaxLength(20);

            entity.HasOne(d => d.MaNguoiDungNavigation).WithOne(p => p.TaiKhoan)
                .HasForeignKey<TaiKhoan>(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan__MaNguo__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
