using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface ILopHocPhanService
    {
        List<LopHocPhan> GetAll();
        LopHocPhan GetById(string id);
        void Add(LopHocPhan lopHocPhan);
        void Update(LopHocPhan lopHocPhan);
        List<LopHocPhan> GetByHocKy(string maHocKy);
        List<LopHocPhan> GetByHocKyAndNganh(string maHocKy, string maNganh);
    }
}
