using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IDiemCongTacXaHoiService
    {
        List<DiemCongTacXaHoi> GetAll();
        DiemCongTacXaHoi GetBySinhVien(string maSv);
        List<DiemCongTacXaHoi> Search(string term);
        void Update(string maSv, float? tongDiem);
        void Create(string maSv);
    }
}
