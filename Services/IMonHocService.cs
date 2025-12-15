using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IMonHocService
    {
        List<MonHoc> GetAll();
        MonHoc GetById(string id);
        void Add(MonHoc monHoc);
        void Update(MonHoc monHoc);
        void Delete(string id);
    }
}
