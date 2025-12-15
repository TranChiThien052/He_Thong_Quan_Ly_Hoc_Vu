using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class HocKy
{
    public string? MaHocKy { get; set; } = null!;

    public string NamHoc { get; set; } = null!;

    public int HocKySo { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public virtual ICollection<DiemRenLuyen> DiemRenLuyens { get; set; } = new List<DiemRenLuyen>();

    public virtual ICollection<LopHocPhan> LopHocPhans { get; set; } = new List<LopHocPhan>();
}
