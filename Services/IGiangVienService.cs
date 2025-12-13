using QuanLyHocVu.Models;
using System.Collections.Generic;

namespace QuanLyHocVu.Services
{
    public interface IGiangVienService
    {
        List<GiangVien> GetAll();
        GiangVien GetById(string id);
        void Add(GiangVien giangVien);
        void Update(GiangVien giangVien);
        void Delete(string id);
        string GenerateGiangVienId(GiangVien giangVien);
    }
}
