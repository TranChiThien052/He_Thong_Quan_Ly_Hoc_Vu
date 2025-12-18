using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IDiemRenLuyenService
    {
        List<DiemRenLuyen> GetAll();
        List<DiemRenLuyen> GetBySinhVien(string maSv);
        List<DiemRenLuyen> GetByHocKy(string maHocKy);
        List<DiemRenLuyen> Search(string term);
        DiemRenLuyen GetById(string maSv, string maHocKy);
        void Update(string maSv, string maHocKy, int? diem);
        void CreateForSinhVien(string maSv);
    }
}
