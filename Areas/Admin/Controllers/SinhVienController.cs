using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SinhVienController : Controller
    {
        private readonly ISinhVienService _sinhVienService;
        private readonly INganhService _nganhService;

        public SinhVienController(ISinhVienService sinhVienService, INganhService nganhService)
        {
            _sinhVienService = sinhVienService;
            _nganhService = nganhService;
        }

        public IActionResult Index(string nienKhoa, string maNganh, string maSinhVien)
        {
            var sinhViens = _sinhVienService.GetAll();

            if (!string.IsNullOrEmpty(nienKhoa))
            {
                sinhViens = sinhViens.Where(s => s.NienKhoa != null && s.NienKhoa.Contains(nienKhoa)).ToList();
            }

            if (!string.IsNullOrEmpty(maNganh))
            {
                sinhViens = sinhViens.Where(s => s.MaNganh == maNganh).ToList();
            }

            if (!string.IsNullOrEmpty(maSinhVien))
            {
                sinhViens = sinhViens.Where(s => s.MaNguoiDung.Contains(maSinhVien)).ToList();
            }

            ViewBag.MaNganh = new SelectList(_nganhService.GetAll(), "MaNganh", "TenNganh");
            ViewBag.CurrentNienKhoa = nienKhoa;
            ViewBag.CurrentMaNganh = maNganh;
            ViewBag.CurrentMaSinhVien = maSinhVien;

            return View(sinhViens);
        }

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

        public IActionResult Create()
        {
            var nganhs = _nganhService.GetAll();
            ViewData["MaNganh"] = new SelectList(nganhs, "MaNganh", "TenNganh");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("HoTen,QueQuan,NgaySinh,Email,SoDienThoai,Cccd,DiaChiThuongTru,DiaChiTamTru,MaNganh,NienKhoa,TinhTrangHoc")] QuanLyHocVu.Models.SinhVien sinhVien)
        {
            ModelState.Remove("MaNganhNavigation");
            ModelState.Remove("TaiKhoan");
            ModelState.Remove("MaNguoiDung");

            if (ModelState.IsValid)
            {
                sinhVien.MaNguoiDung = _sinhVienService.GenerateStudentId(sinhVien);
                
                _sinhVienService.Add(sinhVien);
                return RedirectToAction(nameof(Index));
            }
            var nganhs = _nganhService.GetAll();
            ViewData["MaNganh"] = new SelectList(nganhs, "MaNganh", "TenNganh", sinhVien.MaNganh);
            return View(sinhVien);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("HoTen,QueQuan,NgaySinh,Email,SoDienThoai,Cccd,DiaChiThuongTru,DiaChiTamTru,MaNganh,NienKhoa,TinhTrangHoc")] QuanLyHocVu.Models.SinhVien sinhVien)
        {
            if (id != sinhVien.MaNguoiDung)
            {
                return NotFound();
            }

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
    }
}
