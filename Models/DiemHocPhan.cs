using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class DiemHocPhan
{
    public string MaSinhVien { get; set; } = null!;

    public string MaLopHocPhan { get; set; } = null!;

    public double? DiemChuyenCan { get; set; }

    public double? DiemGiuaKy { get; set; }

    public double? DiemCuoiKy { get; set; }

    public virtual LopHocPhan MaLopHocPhanNavigation { get; set; } = null!;

    public virtual SinhVien MaSinhVienNavigation { get; set; } = null!;
}
