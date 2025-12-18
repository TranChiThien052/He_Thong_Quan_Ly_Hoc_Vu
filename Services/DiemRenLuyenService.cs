using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public class DiemRenLuyenService : IDiemRenLuyenService
    {
        private readonly QuanLyHocVuContext _context;

        public DiemRenLuyenService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<DiemRenLuyen> GetAll()
        {
            return _context.DiemRenLuyens
                .Include(d => d.MaSinhVienNavigation)
                .Include(d => d.MaHocKyNavigation)
                .ToList();
        }

        public List<DiemRenLuyen> GetBySinhVien(string maSv)
        {
            return _context.DiemRenLuyens
                .Where(d => d.MaSinhVien == maSv)
                .Include(d => d.MaHocKyNavigation)
                .ToList();
        }

        public List<DiemRenLuyen> GetByHocKy(string maHocKy)
        {
            return _context.DiemRenLuyens
                .Where(d => d.MaHocKy == maHocKy)
                .Include(d => d.MaSinhVienNavigation)
                .ToList();
        }
        
        public List<DiemRenLuyen> Search(string term)
        {
             if (string.IsNullOrEmpty(term))
            {
                return GetAll();
            }

            term = term.ToLower();
            return _context.DiemRenLuyens
                .Include(d => d.MaSinhVienNavigation)
                .Include(d => d.MaHocKyNavigation)
                .Where(d => d.MaSinhVien.ToLower().Contains(term) || 
                            d.MaSinhVienNavigation.HoTen.ToLower().Contains(term) ||
                            d.MaHocKy.ToLower().Contains(term))
                .ToList();
        }

        public DiemRenLuyen GetById(string maSv, string maHocKy)
        {
            return _context.DiemRenLuyens
                .Include(d => d.MaSinhVienNavigation)
                .Include(d => d.MaHocKyNavigation)
                .FirstOrDefault(d => d.MaSinhVien == maSv && d.MaHocKy == maHocKy);
        }

        public void Update(string maSv, string maHocKy, int? diem)
        {
            var drl = _context.DiemRenLuyens.Find(maSv, maHocKy);
            if (drl != null)
            {
                drl.Diem = diem;
                _context.SaveChanges();
            }
        }

        public void CreateForSinhVien(string maSv) // Potentially useful if we need to backfill
        {
             // This logic depends on business rule: create for all semesters? or current?
             // Leaving empty or basic for now as requirement is focused on input/edit and list
             // However, for consistency, if a student is added, we typically add DRL for current semester?
             // User request: "sửa lại chức năng thêm một sinh viên sẽ tự động tạo model điểm công tác xã hội"
             // User did NOT ask to auto create DiemRenLuyen here (it was in previous convo for Semester creation).
             // But for completeness, good to have placeholders.
        }
    }
}
