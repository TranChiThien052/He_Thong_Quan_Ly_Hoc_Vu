using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class Khoa
{
    public string MaKhoa { get; set; } = null!;

    public string TenKhoa { get; set; } = null!;

    public virtual ICollection<GiangVien> GiangViens { get; set; } = new List<GiangVien>();

    public virtual ICollection<Nganh> MaNganhs { get; set; } = new List<Nganh>();
}
