using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyHocVu.Services
{
    public class ChuongTrinhDaoTaoService : IChuongTrinhDaoTaoService
    {
        private readonly QuanLyHocVuContext _context;

        public ChuongTrinhDaoTaoService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<ChuongTrinhDaoTao> GetAll()
        {
            return _context.ChuongTrinhDaoTaos
                .Include(c => c.MaNganhNavigation)
                .Include(c => c.ChiTietChuongTrinhDaoTaos)
                .ThenInclude(ct => ct.MaMonHocNavigation)
                .ToList();
        }

        public ChuongTrinhDaoTao GetById(string id)
        {
            return _context.ChuongTrinhDaoTaos
                .Include(c => c.MaNganhNavigation)
                .FirstOrDefault(c => c.MaCtdt == id);
        }

        public void Add(ChuongTrinhDaoTao ctdt)
        {
            _context.ChuongTrinhDaoTaos.Add(ctdt);
            _context.SaveChanges();
        }

        public void Update(ChuongTrinhDaoTao ctdt)
        {
            _context.ChuongTrinhDaoTaos.Update(ctdt);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var ctdt = _context.ChuongTrinhDaoTaos.FirstOrDefault(c => c.MaCtdt == id);
            if (ctdt != null)
            {
                _context.ChuongTrinhDaoTaos.Remove(ctdt);
                _context.SaveChanges();
            }
        }
    }
}
