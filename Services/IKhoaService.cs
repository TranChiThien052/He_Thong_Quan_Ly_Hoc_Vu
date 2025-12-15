using QuanLyHocVu.Models;

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
