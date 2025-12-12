using QuanLyHocVu.Models;
using System.Collections.Generic;

namespace QuanLyHocVu.Services
{
    public interface IKhoaService
    {
        List<Khoa> GetAll();
        Khoa GetById(string id);
        void Add(Khoa khoa);
        void Update(Khoa khoa);
        void Delete(string id);
    }
}
