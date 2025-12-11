using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class DangKyHocPhan
{
    public string MaSinhVien { get; set; } = null!;

    public string MaLopHocPhan { get; set; } = null!;

    public string? TrangThai { get; set; }

    public virtual LopHocPhan MaLopHocPhanNavigation { get; set; } = null!;

    public virtual SinhVien MaSinhVienNavigation { get; set; } = null!;
}
