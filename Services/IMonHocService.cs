using QuanLyHocVu.Models;
using System.Collections.Generic;

namespace QuanLyHocVu.Services
{
    public interface IMonHocService
    {
        List<MonHoc> GetAll();
        MonHoc GetById(string id);
        // Sau này có thể thêm: List<MonHoc> GetBySinhVien(string maSV);
    }
}
