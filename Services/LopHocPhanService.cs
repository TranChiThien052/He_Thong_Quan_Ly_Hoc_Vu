using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyHocVu.Services
{
    public class LopHocPhanService : ILopHocPhanService
    {
        private readonly QuanLyHocVuContext _context;

        public LopHocPhanService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<LopHocPhan> GetAll()
        {
            return _context.LopHocPhans
                .Include(l => l.MaMonHocNavigation)
                .Include(l => l.MaGiangVienNavigation)
                .Include(l => l.MaHocKyNavigation)
                .Include(l => l.PhongHocNavigation)
                .OrderBy(l => l.MaHocKy)
                .ToList();
        }

        public LopHocPhan GetById(string id)
        {
            return _context.LopHocPhans
                .Include(l => l.MaMonHocNavigation)
                .Include(l => l.MaGiangVienNavigation)
                .Include(l => l.MaHocKyNavigation)
                .Include(l => l.PhongHocNavigation)
                .FirstOrDefault(l => l.MaLopHocPhan == id);
        }

        public void Add(LopHocPhan lopHocPhan)
        {
            _context.LopHocPhans.Add(lopHocPhan);
            _context.SaveChanges();
        }

        public void Update(LopHocPhan lopHocPhan)
        {
            _context.LopHocPhans.Update(lopHocPhan);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var lopHocPhan = _context.LopHocPhans.FirstOrDefault(l => l.MaLopHocPhan == id);
            if (lopHocPhan != null)
            {
                _context.LopHocPhans.Remove(lopHocPhan);
                _context.SaveChanges();
            }
        }

        public List<LopHocPhan> GetByHocKy(string maHocKy)
        {
            return _context.LopHocPhans
                .Include(l => l.MaMonHocNavigation)
                .Include(l => l.MaGiangVienNavigation)
                .Include(l => l.MaHocKyNavigation)
                .Include(l => l.PhongHocNavigation)
                .Where(l => l.MaHocKy == maHocKy)
                .ToList();
        }

        public List<LopHocPhan> GetByHocKyAndNganh(string maHocKy, string maNganh)
        {
            var query = _context.LopHocPhans
                .Include(l => l.MaMonHocNavigation)
                    .ThenInclude(m => m.ChiTietChuongTrinhDaoTaos)
                        .ThenInclude(ct => ct.MaCtdtNavigation)
                .Include(l => l.MaGiangVienNavigation)
                .Include(l => l.MaHocKyNavigation)
                .Include(l => l.PhongHocNavigation)
                .Where(l => l.MaHocKy == maHocKy && l.MaMonHocNavigation.ChiTietChuongTrinhDaoTaos
                    .Any(ct => ct.MaCtdtNavigation.MaNganh == maNganh));
                    
            return query
                .Distinct()
                .ToList();
        }
    }
}
