using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class GiangVien
{
    public string MaNguoiDung { get; set; } = null!;

    public string? ChuyenMon { get; set; }

    public string MaKhoa { get; set; } = null!;

    public string? TinhTrangCongTac { get; set; }

    public virtual ICollection<LopHocPhan> LopHocPhans { get; set; } = new List<LopHocPhan>();

    public virtual Khoa MaKhoaNavigation { get; set; } = null!;

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
