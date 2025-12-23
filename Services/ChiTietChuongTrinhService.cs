using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyHocVu.Services
{
    public class ChiTietChuongTrinhService : IChiTietChuongTrinhService
    {
        private readonly QuanLyHocVuContext _context;

        public ChiTietChuongTrinhService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<ChiTietChuongTrinhDaoTao> GetByMaCTDT(string maCTDT)
        {
            return _context.ChiTietChuongTrinhDaoTaos
                .Include(c => c.MaMonHocNavigation)
                .Where(c => c.MaCtdt == maCTDT)
                .ToList();
        }

        public void UpdateChiTiet(string maCTDT, List<ChiTietChuongTrinhDaoTao> chiTietList)
        {
            var existingItems = _context.ChiTietChuongTrinhDaoTaos.Where(c => c.MaCtdt == maCTDT);
            _context.ChiTietChuongTrinhDaoTaos.RemoveRange(existingItems);
            
            foreach(var item in chiTietList)
            {
                item.MaCtdt = maCTDT;
                _context.ChiTietChuongTrinhDaoTaos.Add(item);
            }
            
            _context.SaveChanges();
        }
    }
}
