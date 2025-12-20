using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyHocVu.Services
{
    public class DangKyHocPhanService : IDangKyHocPhanService
    {
        private readonly QuanLyHocVuContext _context;

        public DangKyHocPhanService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<DangKyHocPhan> GetAll()
        {
            return _context.DangKyHocPhans
                .Include(d => d.MaSinhVienNavigation)
                .Include(d => d.MaLopHocPhanNavigation)
                .ToList();
        }

        public DangKyHocPhan GetById(string maSinhVien, string maLopHocPhan)
        {
            return _context.DangKyHocPhans
                .Include(d => d.MaSinhVienNavigation)
                .Include(d => d.MaLopHocPhanNavigation)
                .FirstOrDefault(d => d.MaSinhVien == maSinhVien && d.MaLopHocPhan == maLopHocPhan);
        }

        public List<DangKyHocPhan> GetBySinhVien(string maSinhVien)
        {
             return _context.DangKyHocPhans
                .Include(d => d.MaLopHocPhanNavigation)
                    .ThenInclude(l => l.MaMonHocNavigation)
                .Include(d => d.MaLopHocPhanNavigation)
                    .ThenInclude(l => l.MaGiangVienNavigation)
                .Where(d => d.MaSinhVien == maSinhVien)
                .ToList();
        }

        public void Add(DangKyHocPhan dangKyHocPhan)
        {
            _context.DangKyHocPhans.Add(dangKyHocPhan);
            _context.SaveChanges();
        }

        public void Update(DangKyHocPhan dangKyHocPhan)
        {
            _context.DangKyHocPhans.Update(dangKyHocPhan);
            _context.SaveChanges();
        }

        public void Delete(string maSinhVien, string maLopHocPhan)
        {
            var dangKyHocPhan = _context.DangKyHocPhans
                .FirstOrDefault(d => d.MaSinhVien == maSinhVien && d.MaLopHocPhan == maLopHocPhan);
            if (dangKyHocPhan != null)
            {
                _context.DangKyHocPhans.Remove(dangKyHocPhan);
                _context.SaveChanges();
            }
        }
    }
}
