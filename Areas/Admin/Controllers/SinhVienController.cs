using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SinhVienController : Controller
    {
        private readonly ISinhVienService _sinhVienService;
        private readonly INganhService _nganhService;

        public SinhVienController(ISinhVienService sinhVienService, INganhService nganhService)
        {
            _sinhVienService = sinhVienService;
            _nganhService = nganhService;
        }

        // GET: Admin/SinhVien
        public IActionResult Index(string nienKhoa, string maNganh, string maSinhVien)
        {
            var sinhViens = _sinhVienService.GetAll();

            // Lọc theo Niên khóa
            if (!string.IsNullOrEmpty(nienKhoa))
            {
                sinhViens = sinhViens.Where(s => s.NienKhoa != null && s.NienKhoa.Contains(nienKhoa)).ToList();
            }

            // Lọc theo Ngành
            if (!string.IsNullOrEmpty(maNganh))
            {
                sinhViens = sinhViens.Where(s => s.MaNganh == maNganh).ToList();
            }

            // Lọc theo Mã sinh viên
            if (!string.IsNullOrEmpty(maSinhVien))
            {
                sinhViens = sinhViens.Where(s => s.MaNguoiDung.Contains(maSinhVien)).ToList();
            }

            // Truyền danh sách ngành cho dropdown
            ViewBag.MaNganh = new SelectList(_nganhService.GetAll(), "MaNganh", "TenNganh");
            ViewBag.CurrentNienKhoa = nienKhoa;
            ViewBag.CurrentMaNganh = maNganh;
            ViewBag.CurrentMaSinhVien = maSinhVien;

            return View(sinhViens);
        }

        // GET: Admin/SinhVien/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = _sinhVienService.GetById(id);

            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: Admin/SinhVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("HoTen,QueQuan,NgaySinh,Email,SoDienThoai,Cccd,DiaChiThuongTru,DiaChiTamTru,MaNganh,NienKhoa,TinhTrangHoc")] QuanLyHocVu.Models.SinhVien sinhVien)
        {
            // Bỏ qua xác thực cho các thuộc tính navigation và MaNguoiDung (vì sẽ tự sinh)
            ModelState.Remove("MaNganhNavigation");
            ModelState.Remove("TaiKhoan");
            ModelState.Remove("MaNguoiDung");

            if (ModelState.IsValid)
            {
                // Tự động sinh Mã sinh viên
                sinhVien.MaNguoiDung = _sinhVienService.GenerateStudentId(sinhVien);
                
                _sinhVienService.Add(sinhVien);
                return RedirectToAction(nameof(Index));
            }
            var nganhs = _nganhService.GetAll();
            ViewData["MaNganh"] = new SelectList(nganhs, "MaNganh", "TenNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

        // GET: Admin/SinhVien/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = _sinhVienService.GetById(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            var nganhs = _nganhService.GetAll();
            ViewData["MaNganh"] = new SelectList(nganhs, "MaNganh", "TenNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

        // POST: Admin/SinhVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("MaNguoiDung,HoTen,QueQuan,NgaySinh,Email,SoDienThoai,Cccd,DiaChiThuongTru,DiaChiTamTru,MaNganh,NienKhoa,TinhTrangHoc")] QuanLyHocVu.Models.SinhVien sinhVien)
        {
            if (id != sinhVien.MaNguoiDung)
            {
                return NotFound();
            }

            // Bỏ qua xác thực cho các thuộc tính navigation
            ModelState.Remove("MaNganhNavigation");
            ModelState.Remove("TaiKhoan");

            if (ModelState.IsValid)
            {
                try
                {
                    _sinhVienService.Update(sinhVien);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_sinhVienService.GetById(sinhVien.MaNguoiDung) == null)
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
            var nganhs = _nganhService.GetAll();
            ViewData["MaNganh"] = new SelectList(nganhs, "MaNganh", "TenNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

        // GET: Admin/SinhVien/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = _sinhVienService.GetById(id);

            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: Admin/SinhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
