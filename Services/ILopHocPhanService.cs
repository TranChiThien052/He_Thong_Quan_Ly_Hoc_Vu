using QuanLyHocVu.Models;
using System.Collections.Generic;

namespace QuanLyHocVu.Services
{
    public interface ILopHocPhanService
    {
        List<LopHocPhan> GetAll();
        LopHocPhan GetById(string id);
        void Add(LopHocPhan lopHocPhan);
        void Update(LopHocPhan lopHocPhan);
        void Delete(string id);
    }
}
