using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface ITaiKhoanService
    {
        TaiKhoan? GetById(string maNguoiDung);
        void Add(TaiKhoan taiKhoan);
        void Update(TaiKhoan taiKhoan);
        bool Exists(string maNguoiDung);
    }
}
