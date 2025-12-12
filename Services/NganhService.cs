using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyHocVu.Services
{
    public class NganhService : INganhService
    {
        private readonly QuanLyHocVuContext _context;

        public NganhService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<Nganh> GetAll()
        {
            return _context.Nganhs
                .Include(n => n.MaKhoas)
                .ToList();
        }

        public Nganh GetById(string id)
        {
            return _context.Nganhs
                .Include(n => n.MaKhoas)
                .FirstOrDefault(n => n.MaNganh == id);
        }

        public void Add(Nganh nganh)
        {
            _context.Nganhs.Add(nganh);
            _context.SaveChanges();
        }

        public void Update(Nganh nganh)
        {
            _context.Nganhs.Update(nganh);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var nganh = _context.Nganhs.FirstOrDefault(n => n.MaNganh == id);
            if (nganh != null)
            {
                _context.Nganhs.Remove(nganh);
                _context.SaveChanges();
            }
        }
    }
}
