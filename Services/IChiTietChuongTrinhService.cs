using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IChiTietChuongTrinhService
    {
        List<ChiTietChuongTrinhDaoTao> GetByMaCTDT(string maCTDT);
        void UpdateChiTiet(string maCTDT, List<ChiTietChuongTrinhDaoTao> chiTietList);
    }
}
