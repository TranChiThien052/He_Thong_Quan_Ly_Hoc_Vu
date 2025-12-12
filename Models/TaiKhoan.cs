using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class TaiKhoan
{
    public string MaNguoiDung { get; set; } = null!;

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? TrangThai { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
