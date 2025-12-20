using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class DiemHocPhan
{
    public string MaSinhVien { get; set; } = null!;

    public string MaLopHocPhan { get; set; } = null!;

    public decimal? DiemChuyenCan { get; set; }

    public decimal? DiemGiuaKy { get; set; }

    public decimal? DiemCuoiKy { get; set; }

    public virtual LopHocPhan MaLopHocPhanNavigation { get; set; } = null!;

    public virtual SinhVien MaSinhVienNavigation { get; set; } = null!;
}
