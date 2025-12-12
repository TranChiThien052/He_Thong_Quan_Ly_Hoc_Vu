using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class SinhVienHoatDongCtxh
{
    public string MaSinhVien { get; set; } = null!;

    public string MaHoatDong { get; set; } = null!;

    public DateOnly? NgayThamGia { get; set; }

    public int? DiemThucTe { get; set; }

    public string? GhiChu { get; set; }

    public virtual HoatDongCtxh MaHoatDongNavigation { get; set; } = null!;

    public virtual SinhVien MaSinhVienNavigation { get; set; } = null!;
}
