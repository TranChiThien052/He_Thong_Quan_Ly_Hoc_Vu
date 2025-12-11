using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class PhongHoc
{
    public string MaPhong { get; set; } = null!;

    public int Tang { get; set; }

    public string Khu { get; set; } = null!;

    public string LoaiPhong { get; set; } = null!;

    public virtual ICollection<LopHocPhan> LopHocPhans { get; set; } = new List<LopHocPhan>();
}
