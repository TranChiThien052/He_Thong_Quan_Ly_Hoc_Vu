namespace QuanLyHocVu.Models;

public partial class ChiTietChuongTrinhDaoTao
{
    public string MaCtdt { get; set; } = null!;

    public string MaMonHoc { get; set; } = null!;

    public int HocKy { get; set; }

    public virtual ChuongTrinhDaoTao MaCtdtNavigation { get; set; } = null!;

    public virtual MonHoc MaMonHocNavigation { get; set; } = null!;
}