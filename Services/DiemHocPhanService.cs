using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyHocVu.Services
{
    public class DiemHocPhanService : IDiemHocPhanService
    {
        private readonly QuanLyHocVuContext _context;

        public DiemHocPhanService(QuanLyHocVuContext context)
        {
            _context = context;
        }
        
        public void Add(DiemHocPhan diemHocPhan)
        {
            _context.DiemHocPhans.Add(diemHocPhan);
            _context.SaveChanges();
        }

        public List<DiemHocPhan> GetByLopHocPhan(string maLopHocPhan)
        {
            return _context.DiemHocPhans
                .Include(d => d.MaSinhVienNavigation)
                .Where(d => d.MaLopHocPhan == maLopHocPhan)
                .ToList();
        }

        public DiemHocPhan GetById(string maSinhVien, string maLopHocPhan){
            return _context.DiemHocPhans
                .Include(d=>d.MaSinhVienNavigation)
                .OrderBy(d => d.MaSinhVienNavigation.HoTen)
                .FirstOrDefault(d => d.MaSinhVien == maSinhVien && d.MaLopHocPhan == maLopHocPhan);
        }

        public void UpdateDiem(DiemHocPhan diemHocPhan)
        {
            var existingDiem = _context.DiemHocPhans
                .Include(d=>d.MaSinhVienNavigation)
                .FirstOrDefault(d => d.MaSinhVien == diemHocPhan.MaSinhVien && d.MaLopHocPhan == diemHocPhan.MaLopHocPhan);

            if (existingDiem != null)
            {
                existingDiem.DiemChuyenCan = diemHocPhan.DiemChuyenCan;
                existingDiem.DiemGiuaKy = diemHocPhan.DiemGiuaKy;
                existingDiem.DiemCuoiKy = diemHocPhan.DiemCuoiKy;
                _context.SaveChanges();
            }
        }

        public void Delete(string maSinhVien, string maLopHocPhan)
        {
            var diemHocPhan = _context.DiemHocPhans
                .FirstOrDefault(d => d.MaSinhVien == maSinhVien && d.MaLopHocPhan == maLopHocPhan);

            if (diemHocPhan != null)
            {
                _context.DiemHocPhans.Remove(diemHocPhan);
                _context.SaveChanges();
            }
        }

        public List<DiemHocPhan> GetBySinhVienAndHocKy(string maSinhVien, string maHocKy)
        {
            return _context.DiemHocPhans
                .Include(d => d.MaLopHocPhanNavigation)
                .ThenInclude(l => l.MaMonHocNavigation)
                .Include(d => d.MaLopHocPhanNavigation)
                .ThenInclude(l => l.MaGiangVienNavigation)
                .Where(d => d.MaSinhVien == maSinhVien && d.MaLopHocPhanNavigation.MaHocKy == maHocKy)
                .ToList();
        }
    }
}
