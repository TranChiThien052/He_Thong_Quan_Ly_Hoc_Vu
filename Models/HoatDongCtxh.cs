using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class HoatDongCtxh
{
    public string MaHoatDong { get; set; } = null!;

    public string? TenHoatDong { get; set; }

    public int? Diem { get; set; }

    public DateOnly? NgayToChuc { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<SinhVienHoatDongCtxh> SinhVienHoatDongCtxhs { get; set; } = new List<SinhVienHoatDongCtxh>();
}
