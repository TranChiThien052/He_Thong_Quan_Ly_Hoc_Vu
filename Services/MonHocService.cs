using QuanLyHocVu.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyHocVu.Services
{
    public class MonHocService : IMonHocService
    {
        private readonly QuanLyHocVuContext _context;

        public MonHocService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<MonHoc> GetAll()
        {
            return _context.MonHocs.ToList();
        }

        public MonHoc GetById(string id)
        {
            return _context.MonHocs.FirstOrDefault(m => m.MaMonHoc == id);
        }
    }
}
