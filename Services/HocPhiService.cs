using QuanLyHocVu.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyHocVu.Services
{
    public class HocPhiService : IHocPhiService
    {
        private readonly QuanLyHocVuContext _context;

        public HocPhiService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<HocPhi> GetAll()
        {
            return _context.HocPhis.ToList();
        }

        public HocPhi GetById(string id)
        {
            return _context.HocPhis.FirstOrDefault(h => h.MaHocPhi == id);
        }

        public void Add(HocPhi hocPhi)
        {
            _context.HocPhis.Add(hocPhi);
            _context.SaveChanges();
        }

        public void Update(HocPhi hocPhi)
        {
            _context.HocPhis.Update(hocPhi);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var hocPhi = _context.HocPhis.FirstOrDefault(h => h.MaHocPhi == id);
            if (hocPhi != null)
            {
                _context.HocPhis.Remove(hocPhi);
                _context.SaveChanges();
            }
        }
    }
}
