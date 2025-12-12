using QuanLyHocVu.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyHocVu.Services
{
    public class HocKyService : IHocKyService
    {
        private readonly QuanLyHocVuContext _context;

        public HocKyService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<HocKy> GetAll()
        {
            return _context.HocKies.ToList();
        }

        public HocKy GetById(string id)
        {
            return _context.HocKies.FirstOrDefault(h => h.MaHocKy == id);
        }

        public void Add(HocKy hocKy)
        {
            _context.HocKies.Add(hocKy);
            _context.SaveChanges();
        }

        public void Update(HocKy hocKy)
        {
            _context.HocKies.Update(hocKy);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var hocKy = _context.HocKies.FirstOrDefault(h => h.MaHocKy == id);
            if (hocKy != null)
            {
                _context.HocKies.Remove(hocKy);
                _context.SaveChanges();
            }
        }
    }
}
