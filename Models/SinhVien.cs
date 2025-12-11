using System;
using System.Collections.Generic;

namespace QuanLyHocVu.Models;

public partial class SinhVien
{
    public string MaNguoiDung { get; set; } = null!;

    public string MaNganh { get; set; } = null!;

    public string? NienKhoa { get; set; }

    public string? TinhTrangHoc { get; set; }

    public virtual ICollection<DangKyHocPhan> DangKyHocPhans { get; set; } = new List<DangKyHocPhan>();

    public virtual DiemCongTacXaHoi? DiemCongTacXaHoi { get; set; }

    public virtual ICollection<DiemRenLuyen> DiemRenLuyens { get; set; } = new List<DiemRenLuyen>();

    public virtual Nganh MaNganhNavigation { get; set; } = null!;

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual ICollection<SinhVienHoatDongCtxh> SinhVienHoatDongCtxhs { get; set; } = new List<SinhVienHoatDongCtxh>();
}
