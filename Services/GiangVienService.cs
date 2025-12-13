using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyHocVu.Services
{
    public class GiangVienService : IGiangVienService
    {
        private readonly QuanLyHocVuContext _context;

        public GiangVienService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<GiangVien> GetAll()
        {
            return _context.GiangViens
                .Include(g => g.MaKhoaNavigation)
                .Include(g => g.TaiKhoan)
                .ToList();
        }

        public GiangVien GetById(string id)
        {
            return _context.GiangViens
                .Include(g => g.MaKhoaNavigation)
                .Include(g => g.TaiKhoan)
                .FirstOrDefault(g => g.MaNguoiDung == id);
        }

        public void Add(GiangVien giangVien)
        {
            _context.GiangViens.Add(giangVien);
            _context.SaveChanges();
        }

        public void Update(GiangVien giangVien)
        {
            _context.GiangViens.Update(giangVien);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var giangVien = _context.GiangViens.FirstOrDefault(g => g.MaNguoiDung == id);
            if (giangVien != null)
            {
                _context.GiangViens.Remove(giangVien);
                _context.SaveChanges();
            }
        }

        public string GenerateGiangVienId(GiangVien giangVien)
        {
            // 1. Số thứ tự Khoa
            var khoas = _context.Khoas.OrderBy(k => k.MaKhoa).Select(k => k.MaKhoa).ToList();
            int khoaIndex = khoas.IndexOf(giangVien.MaKhoa) + 1;

            // 2. Số thứ tự Giảng viên trong Khoa
            int count = _context.GiangViens.Count(g => g.MaKhoa == giangVien.MaKhoa);
            int sequence = count + 1;

            // Format: GV + KhoaIndex(2 so) + Sequence(4 so)
            return $"GV{khoaIndex:D2}{sequence:D4}";
        }
    }
}
