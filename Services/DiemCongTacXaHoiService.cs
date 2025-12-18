using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public class DiemCongTacXaHoiService : IDiemCongTacXaHoiService
    {
        private readonly QuanLyHocVuContext _context;

        public DiemCongTacXaHoiService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<DiemCongTacXaHoi> GetAll()
        {
            return _context.DiemCongTacXaHois
                .Include(d => d.MaSinhVienNavigation)
                .ToList();
        }

        public DiemCongTacXaHoi GetBySinhVien(string maSv)
        {
            return _context.DiemCongTacXaHois
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefault(d => d.MaSinhVien == maSv);
        }

        public List<DiemCongTacXaHoi> Search(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return GetAll();
            }
            term = term.ToLower();
            return _context.DiemCongTacXaHois
                .Include(d => d.MaSinhVienNavigation)
                .Where(d => d.MaSinhVien.ToLower().Contains(term) || 
                            d.MaSinhVienNavigation.HoTen.ToLower().Contains(term))
                .ToList();
        }

        public void Update(string maSv, int? tongDiem)
        {
            var ctxh = _context.DiemCongTacXaHois.Find(maSv);
            if (ctxh != null)
            {
                ctxh.TongDiem = tongDiem;
                _context.SaveChanges();
            }
             else 
            {
                // If it doesn't exist (maybe old data), create it
                ctxh = new DiemCongTacXaHoi { MaSinhVien = maSv, TongDiem = tongDiem};
                _context.DiemCongTacXaHois.Add(ctxh);
                _context.SaveChanges();
            }
        }

        public void Create(string maSv)
        {
            if (!_context.DiemCongTacXaHois.Any(d => d.MaSinhVien == maSv))
            {
                var ctxh = new DiemCongTacXaHoi 
                { 
                    MaSinhVien = maSv, 
                    TongDiem = 0,
                };
                _context.DiemCongTacXaHois.Add(ctxh);
                _context.SaveChanges();
            }
        }
    }
}
