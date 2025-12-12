using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class LopHocPhan
{
    public string MaLopHocPhan { get; set; } = null!;

    public string MaMonHoc { get; set; } = null!;

    public string MaGiangVien { get; set; } = null!;

    public string MaHocKy { get; set; } = null!;

    public string CaHoc { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public string? PhongHoc { get; set; }

    public virtual ICollection<DangKyHocPhan> DangKyHocPhans { get; set; } = new List<DangKyHocPhan>();

    public virtual GiangVien MaGiangVienNavigation { get; set; } = null!;

    public virtual HocKy MaHocKyNavigation { get; set; } = null!;

    public virtual MonHoc MaMonHocNavigation { get; set; } = null!;

    public virtual PhongHoc? PhongHocNavigation { get; set; }
}
