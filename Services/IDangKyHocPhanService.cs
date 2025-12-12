using QuanLyHocVu.Models;
using System.Collections.Generic;

namespace QuanLyHocVu.Services
{
    public interface IDangKyHocPhanService
    {
        List<DangKyHocPhan> GetAll();
        DangKyHocPhan GetById(string maSinhVien, string maLopHocPhan);
        List<DangKyHocPhan> GetBySinhVien(string maSinhVien);
        void Add(DangKyHocPhan dangKyHocPhan);
        void Update(DangKyHocPhan dangKyHocPhan);
        void Delete(string maSinhVien, string maLopHocPhan);
    }
}
