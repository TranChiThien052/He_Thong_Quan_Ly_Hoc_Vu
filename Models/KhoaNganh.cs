using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class KhoaNganh
{
    public string MaKhoa { get; set; } = null!;

    public string MaNganh { get; set; } = null!;

    public virtual Khoa? Khoa { get; set; } = null!;

    public virtual Nganh? Nganh { get; set; } = null!;
}