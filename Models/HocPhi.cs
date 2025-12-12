using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class HocPhi
{
    public string MaHocPhi { get; set; } = null!;

    public decimal? GiaTheoTin { get; set; }

    public virtual ICollection<MonHoc> MonHocs { get; set; } = new List<MonHoc>();
}
