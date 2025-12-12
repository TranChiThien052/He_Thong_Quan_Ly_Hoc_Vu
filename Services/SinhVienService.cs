using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
                .Include(s => s.MaNganhNavigation) // Include major info usually helpful
                .ToList();
        }

        public SinhVien GetById(string id)
        {
            return _context.SinhViens
                .Include(s => s.MaNganhNavigation)
                .FirstOrDefault(s => s.MaNguoiDung == id); // Inherited PK
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
    }
}
