using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IHocKyService
    {
        List<HocKy> GetAll();
        HocKy GetById(string id);
        void Add(HocKy hocKy);
        void Update(HocKy hocKy);
        void Delete(string id);
    }
}
