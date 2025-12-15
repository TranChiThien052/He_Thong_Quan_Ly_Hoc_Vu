using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface IHocPhiService
    {
        List<HocPhi> GetAll();
        HocPhi GetById(string id);
        void Add(HocPhi hocPhi);
        void Update(HocPhi hocPhi);
        void Delete(string id);
    }
}
