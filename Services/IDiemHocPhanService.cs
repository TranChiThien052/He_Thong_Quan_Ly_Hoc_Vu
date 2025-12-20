using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IDiemHocPhanService
    {
        List<DiemHocPhan> GetByLopHocPhan(string maLopHocPhan);
        void UpdateDiem(DiemHocPhan diemHocPhan);
        DiemHocPhan GetById(string maSinhVien, string maLopHocPhan);
    }
}
