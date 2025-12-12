using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class DiemRenLuyen
{
    public string MaSinhVien { get; set; } = null!;

    public string MaHocKy { get; set; } = null!;

    public int? Diem { get; set; }

    public string? XepLoai { get; set; }

    public string? GhiChu { get; set; }

    public virtual HocKy MaHocKyNavigation { get; set; } = null!;

    public virtual SinhVien MaSinhVienNavigation { get; set; } = null!;
}
