using QuanLyHocVu.Models;

namespace QuanLyHocVu.Services
{
    public class PhongHocService : IPhongHocService
    {
        private readonly QuanLyHocVuContext _context;

        public PhongHocService(QuanLyHocVuContext context)
        {
            _context = context;
        }

        public List<PhongHoc> GetAll()
        {
            return _context.PhongHocs.ToList();
        }

        public PhongHoc GetById(string id)
        {
            return _context.PhongHocs.FirstOrDefault(p => p.MaPhong == id);
        }

        public void Add(PhongHoc phongHoc)
        {
            _context.PhongHocs.Add(phongHoc);
            _context.SaveChanges();
        }

        public void Update(PhongHoc phongHoc)
        {
            _context.PhongHocs.Update(phongHoc);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var phongHoc = _context.PhongHocs.FirstOrDefault(p => p.MaPhong == id);
            if (phongHoc != null)
            {
                _context.PhongHocs.Remove(phongHoc);
                _context.SaveChanges();
            }
        }
    }
}
