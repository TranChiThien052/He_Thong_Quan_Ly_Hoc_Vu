using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class MonHoc
{
    public string MaMonHoc { get; set; } = null!;

    public string? TenMonHoc { get; set; }

    public int? SoTinChi { get; set; }

    public string? LoaiMon { get; set; }

    public string MaHocPhi { get; set; } = null!;

    public virtual ICollection<LopHocPhan> LopHocPhans { get; set; } = new List<LopHocPhan>();

    public virtual HocPhi MaHocPhiNavigation { get; set; } = null!;
}
