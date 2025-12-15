using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IKhoaNganhService
    {
        List<KhoaNganh> GetAll();
        void Add(KhoaNganh khoaNganh);
        void Update(KhoaNganh khoaNganh);
        void Delete(KhoaNganh khoaNganh);
    }
}
