using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class CanBo
{
    public string MaNguoiDung { get; set; } = null!;

    public string? TinhTrangCongTac { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
