using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class NguoiDung
{
    public string MaNguoiDung { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? QueQuan { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Cccd { get; set; }

    public string? DiaChiThuongTru { get; set; }

    public string? DiaChiTamTru { get; set; }

    public virtual CanBo? CanBo { get; set; }

    public virtual GiangVien? GiangVien { get; set; }

    public virtual SinhVien? SinhVien { get; set; }

    public virtual TaiKhoan? TaiKhoan { get; set; }
}
