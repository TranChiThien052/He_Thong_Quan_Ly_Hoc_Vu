using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IDiemHocPhanService
    {
        void Add(DiemHocPhan diemHocPhan);
        void Delete(string maSinhVien, string maLopHocPhan);
        List<DiemHocPhan> GetByLopHocPhan(string maLopHocPhan);
        void UpdateDiem(DiemHocPhan diemHocPhan);
        DiemHocPhan GetById(string maSinhVien, string maLopHocPhan);
        List<DiemHocPhan> GetBySinhVienAndHocKy(string maSinhVien, string maHocKy);
    }
}
