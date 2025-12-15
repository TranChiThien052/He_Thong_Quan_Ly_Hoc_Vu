using QuanLyHocVu.Models;

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

        public void Add(MonHoc monHoc)
        {
            _context.MonHocs.Add(monHoc);
            _context.SaveChanges();
        }

        public void Update(MonHoc monHoc)
        {
            _context.MonHocs.Update(monHoc);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var monHoc = _context.MonHocs.FirstOrDefault(m => m.MaMonHoc == id);
            if (monHoc != null)
            {
                _context.MonHocs.Remove(monHoc);
                _context.SaveChanges();
            }
        }
    }
    
}
