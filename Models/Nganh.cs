using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class Nganh
{
    public string MaNganh { get; set; } = null!;

    public string TenNganh { get; set; } = null!;

    public virtual ChuongTrinhDaoTao? ChuongTrinhDaoTao { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();

    public virtual ICollection<KhoaNganh> KhoaNganhs { get; set; } = new List<KhoaNganh>();
}
