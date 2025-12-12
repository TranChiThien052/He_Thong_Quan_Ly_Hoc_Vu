using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class DiemCongTacXaHoi
{
    public string MaSinhVien { get; set; } = null!;

    public int? TongDiem { get; set; }

    public string? GhiChu { get; set; }

    public virtual SinhVien MaSinhVienNavigation { get; set; } = null!;
}
