using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public interface ISinhVienService
    {
        List<SinhVien> GetAll();
        SinhVien GetById(string id);
        void Add(SinhVien sinhVien);
        void Update(SinhVien sinhVien);
        void Delete(string id);
        string GenerateStudentId(SinhVien sinhVien);
    }
}
