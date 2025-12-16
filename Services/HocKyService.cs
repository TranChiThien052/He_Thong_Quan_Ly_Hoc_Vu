using QuanLyHocVu.Models;

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
            
            var activeStudents = _context.SinhViens.Where(sv => sv.TinhTrangHoc == "Đang học").ToList();
            var diemRenLuyens = new List<DiemRenLuyen>();

            foreach (var student in activeStudents)
            {
                diemRenLuyens.Add(new DiemRenLuyen
                {
                    MaSinhVien = student.MaNguoiDung,
                    MaHocKy = hocKy.MaHocKy,
                    Diem = 0,
                    XepLoai = null,
                    GhiChu = null
                });
            }

            _context.DiemRenLuyens.AddRange(diemRenLuyens);
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
