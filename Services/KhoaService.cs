using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public class KhoaService : IKhoaService
    {
        private readonly QuanLyHocVuContext _context;

        public KhoaService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<Khoa> GetAll()
        {
            return _context.Khoas.ToList();
        }

        public Khoa GetById(string id)
        {
            return _context.Khoas.FirstOrDefault(k => k.MaKhoa == id);
        }

        public void Add(Khoa khoa)
        {
            _context.Khoas.Add(khoa);
            _context.SaveChanges();
        }

        public void Update(Khoa khoa)
        {
            _context.Khoas.Update(khoa);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var khoa = _context.Khoas.FirstOrDefault(k => k.MaKhoa == id);
            if (khoa != null)
            {
                _context.Khoas.Remove(khoa);
                _context.SaveChanges();
            }
        }
    }
}
