using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly QuanLyHocVuContext _context;

        public TaiKhoanService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public TaiKhoan? GetById(string maNguoiDung)
        {
            return _context.TaiKhoans
                .Include(t => t.MaNguoiDungNavigation)
                .FirstOrDefault(tk => tk.MaNguoiDung == maNguoiDung);
        }

        public void Add(TaiKhoan taiKhoan)
        {
            _context.Entry(taiKhoan).Reference(t => t.MaNguoiDungNavigation).IsModified = false;
            _context.TaiKhoans.Add(taiKhoan);
            _context.SaveChanges();
        }

        public void Update(TaiKhoan taiKhoan)
        {
            _context.Entry(taiKhoan).Reference(t => t.MaNguoiDungNavigation).IsModified = false;
            _context.TaiKhoans.Update(taiKhoan);
            _context.SaveChanges();
        }

        public bool Exists(string maNguoiDung)
        {
            return _context.TaiKhoans.Any(tk => tk.MaNguoiDung == maNguoiDung);
        }
    }
}