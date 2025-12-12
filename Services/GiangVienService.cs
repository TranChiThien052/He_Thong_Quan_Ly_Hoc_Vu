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
                .ToList();
        }

        public GiangVien GetById(string id)
        {
            return _context.GiangViens
                .Include(g => g.MaKhoaNavigation)
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
    }
}
