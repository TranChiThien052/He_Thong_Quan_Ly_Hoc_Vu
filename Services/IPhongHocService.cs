using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IPhongHocService
    {
        List<PhongHoc> GetAll();
        PhongHoc GetById(string id);
        void Add(PhongHoc phongHoc);
        void Update(PhongHoc phongHoc);
        void Delete(string id);
    }
}
