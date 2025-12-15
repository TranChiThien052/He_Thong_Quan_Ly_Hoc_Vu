using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyHocVu.Services
{
    public class SinhVienService : ISinhVienService
    {
        private readonly QuanLyHocVuContext _context;

        public SinhVienService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<SinhVien> GetAll()
        {
            return _context.SinhViens
                .Include(s => s.MaNganhNavigation)
                .Include(s => s.TaiKhoan)
                .ToList();
        }

        public SinhVien GetById(string id)
        {
            return _context.SinhViens
                .Include(s => s.MaNganhNavigation)
                .Include(s => s.TaiKhoan)
                .FirstOrDefault(s => s.MaNguoiDung == id);
        }

        public void Add(SinhVien sinhVien)
        {
            _context.SinhViens.Add(sinhVien);
            _context.SaveChanges();
        }

        public void Update(SinhVien sinhVien)
        {
            _context.SinhViens.Update(sinhVien);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var sinhVien = _context.SinhViens.FirstOrDefault(s => s.MaNguoiDung == id);
            if (sinhVien != null)
            {
                _context.SinhViens.Remove(sinhVien);
                _context.SaveChanges();
            }
        }

        public string GenerateStudentId(SinhVien sinhVien)
        {
            var nganhs = _context.Nganhs.OrderBy(n => n.MaNganh).Select(n => n.MaNganh).ToList();
            int majorIndex = nganhs.IndexOf(sinhVien.MaNganh) + 1;

            string yearSuffix = DateTime.Now.Year.ToString().Substring(2, 2);

            int count = _context.SinhViens.Count(s => s.MaNganh == sinhVien.MaNganh && s.NienKhoa == sinhVien.NienKhoa);
            int sequence = count + 1;

            return $"DH{majorIndex}{yearSuffix}{sequence:D5}";
        }
    }
}
