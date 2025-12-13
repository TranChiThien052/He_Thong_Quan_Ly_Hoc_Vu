using QuanLyHocVu.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyHocVu.Services
{
    public class CanBoService : ICanBoService
    {
        private readonly QuanLyHocVuContext _context;

        public CanBoService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<CanBo> GetAll()
        {
            return _context.CanBos
                .Include(c => c.TaiKhoan)
                .ToList();
        }

        public CanBo GetById(string id)
        {
            return _context.CanBos
                .Include(c => c.TaiKhoan)
                .FirstOrDefault(c => c.MaNguoiDung == id);
        }

        public void Add(CanBo canBo)
        {
            _context.CanBos.Add(canBo);
            _context.SaveChanges();
        }

        public void Update(CanBo canBo)
        {
            _context.CanBos.Update(canBo);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var canBo = _context.CanBos.FirstOrDefault(c => c.MaNguoiDung == id);
            if (canBo != null)
            {
                _context.CanBos.Remove(canBo);
                _context.SaveChanges();
            }
        }

        public string GenerateCanBoId()
        {
            // Format: CB + Sequence(4 so)
            int count = _context.CanBos.Count();
            int sequence = count + 1;
            return $"CB{sequence:D4}";
        }
    }
}
