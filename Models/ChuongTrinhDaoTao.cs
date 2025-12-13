using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class ChuongTrinhDaoTao
{
    public string MaCtdt { get; set; } = null!;

    public string? TenCtdt { get; set; }

    public string? MaNganh { get; set; }

    public virtual Nganh? MaNganhNavigation { get; set; }

    public virtual ICollection<ChiTietChuongTrinhDaoTao> ChiTietChuongTrinhDaoTaos { get; set; } = new List<ChiTietChuongTrinhDaoTao>();
}
