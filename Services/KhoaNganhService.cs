using QuanLyHocVu.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyHocVu.Services
{
    public class KhoaNganhService : IKhoaNganhService
    {
        private readonly QuanLyHocVuContext _context;

        public KhoaNganhService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<KhoaNganh> GetAll()
        {
            return _context.KhoaNganhs.ToList();
        }
        public KhoaNganh Get(KhoaNganh khoaNganh)
        {
            return _context.KhoaNganhs
                    .FirstOrDefault(k => k.MaKhoa == khoaNganh.MaKhoa && k.MaNganh == khoaNganh.MaNganh);
        }
        public void Add(KhoaNganh khoaNganh)
        {
            _context.KhoaNganhs.Add(khoaNganh);
            _context.SaveChanges();
        }

        public void Update(KhoaNganh khoaNganh)
        {
            _context.KhoaNganhs.Update(khoaNganh);
            _context.SaveChanges();
        }

        public void Delete(KhoaNganh khoaNganh)
        {
            var khoaNganhDelete = _context.KhoaNganhs.FirstOrDefault(k => k.MaKhoa == khoaNganh.MaKhoa && k.MaNganh == khoaNganh.MaNganh);
            if (khoaNganhDelete != null)
            {
                _context.KhoaNganhs.Remove(khoaNganhDelete);
                _context.SaveChanges();
            }
        }
    }
}
