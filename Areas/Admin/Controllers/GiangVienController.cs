using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GiangVienController : Controller
    {
        private readonly IGiangVienService _giangVienService;
        private readonly IKhoaService _khoaService;

        public GiangVienController(IGiangVienService giangVienService, IKhoaService khoaService)
        {
            _giangVienService = giangVienService;
            _khoaService = khoaService;
        }

        // GET: Admin/GiangVien
        public IActionResult Index(string maKhoa, string maGiangVien)
        {
            var giangViens = _giangVienService.GetAll();

            // Lọc theo Khoa
            if (!string.IsNullOrEmpty(maKhoa))
            {
                giangViens = giangViens.Where(g => g.MaKhoa == maKhoa).ToList();
            }

            // Lọc theo Mã giảng viên (MaNguoiDung)
            if (!string.IsNullOrEmpty(maGiangVien))
            {
                giangViens = giangViens.Where(g => g.MaNguoiDung.Contains(maGiangVien)).ToList();
            }

            // Truyền danh sách khoa cho dropdown
            ViewBag.MaKhoa = new SelectList(_khoaService.GetAll(), "MaKhoa", "TenKhoa");
            ViewBag.CurrentMaKhoa = maKhoa;
            ViewBag.CurrentMaGiangVien = maGiangVien;

            return View(giangViens);
        }

        // GET: Admin/GiangVien/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangVien = _giangVienService.GetById(id);

            if (giangVien == null)
            {
                return NotFound();
            }

            return View(giangVien);
        }

        // GET: Admin/GiangVien/Create
        public IActionResult Create()
        {
            var khoas = _khoaService.GetAll();
            ViewData["MaKhoa"] = new SelectList(khoas, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: Admin/GiangVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("HoTen,QueQuan,NgaySinh,Email,SoDienThoai,Cccd,DiaChiThuongTru,DiaChiTamTru,MaKhoa,ChuyenMon,TinhTrangCongTac")] QuanLyHocVu.Models.GiangVien giangVien)
        {
            // Bỏ qua xác thực cho các thuộc tính navigation và MaNguoiDung (vì sẽ tự sinh)
            ModelState.Remove("MaKhoaNavigation");
            ModelState.Remove("TaiKhoan");
            ModelState.Remove("MaNguoiDung");

            if (ModelState.IsValid)
            {
                // Tự động sinh Mã sinh viên
                giangVien.MaNguoiDung = _giangVienService.GenerateGiangVienId(giangVien);
                
                _giangVienService.Add(giangVien);
                return RedirectToAction(nameof(Index));
            }
            var khoas = _khoaService.GetAll();
            ViewData["MaKhoa"] = new SelectList(khoas, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // GET: Admin/GiangVien/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangVien = _giangVienService.GetById(id);
            if (giangVien == null)
            {
                return NotFound();
            }
            var khoas = _khoaService.GetAll();
            ViewData["MaKhoa"] = new SelectList(khoas, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // POST: Admin/GiangVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("MaNguoiDung,HoTen,QueQuan,NgaySinh,Email,SoDienThoai,Cccd,DiaChiThuongTru,DiaChiTamTru,MaKhoa,ChuyenMon,TinhTrangCongTac")] QuanLyHocVu.Models.GiangVien giangVien)
        {
            if (id != giangVien.MaNguoiDung)
            {
                return NotFound();
            }

            // Bỏ qua xác thực cho các thuộc tính navigation
            ModelState.Remove("MaKhoaNavigation");
            ModelState.Remove("TaiKhoan");

            if (ModelState.IsValid)
            {
                try
                {
                    _giangVienService.Update(giangVien);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_giangVienService.GetById(giangVien.MaNguoiDung) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var khoas = _khoaService.GetAll();
            ViewData["MaKhoa"] = new SelectList(khoas, "MaKhoa", "TenKhoa", giangVien.MaKhoa);
            return View(giangVien);
        }

        // GET: Admin/GiangVien/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangVien = _giangVienService.GetById(id);

            if (giangVien == null)
            {
                return NotFound();
            }

            return View(giangVien);
        }

        // POST: Admin/GiangVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
             _giangVienService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
