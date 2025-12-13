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
                .Include(s => s.MaNganhNavigation)
                .Include(s => s.TaiKhoan) // Include account info from base NguoiDung
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
            // 1. Số thứ tự ngành
            var nganhs = _context.Nganhs.OrderBy(n => n.MaNganh).Select(n => n.MaNganh).ToList();
            int majorIndex = nganhs.IndexOf(sinhVien.MaNganh) + 1;

            // 2. 2 số đuôi năm bắt đầu học (lấy năm hiện tại)
            string yearSuffix = DateTime.Now.Year.ToString().Substring(2, 2);

            // 3. Số thứ tự sinh viên trong ngành và niên khóa đó
            int count = _context.SinhViens.Count(s => s.MaNganh == sinhVien.MaNganh && s.NienKhoa == sinhVien.NienKhoa);
            int sequence = count + 1;

            // Format: DH + MaNganh(1 so) + Nam(2 so) + STT(5 so)
            return $"DH{majorIndex}{yearSuffix}{sequence:D5}";
        }
    }
}
