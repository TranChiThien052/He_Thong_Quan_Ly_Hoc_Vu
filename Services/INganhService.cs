using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface INganhService
    {
        List<Nganh> GetAll();
        Nganh GetById(string id);
        void Add(Nganh nganh);
        void Update(Nganh nganh);
        void Delete(string id);
    }
}
