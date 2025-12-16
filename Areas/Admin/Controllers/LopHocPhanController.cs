using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LopHocPhanController : Controller
    {
        private readonly ILopHocPhanService _lopHocPhanService;
        private readonly IGiangVienService _giangVienService;
        private readonly IHocKyService _hocKyService;
        private readonly IMonHocService _monHocService;
        private readonly IPhongHocService _phongHocService;

        public LopHocPhanController(
            ILopHocPhanService lopHocPhanService,
            IGiangVienService giangVienService,
            IHocKyService hocKyService,
            IMonHocService monHocService,
            IPhongHocService phongHocService)
        {
            _lopHocPhanService = lopHocPhanService;
            _giangVienService = giangVienService;
            _hocKyService = hocKyService;
            _monHocService = monHocService;
            _phongHocService = phongHocService;
        }

        public IActionResult Index()
        {
            var data = _lopHocPhanService.GetAll();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.GiangVien = new SelectList(_giangVienService.GetAll(), "MaGiangVien", "HoTen");
            ViewBag.HocKy = new SelectList(_hocKyService.GetAll(), "NamHoc", "HocKySo");
            ViewBag.MonHoc = new SelectList(_monHocService.GetAll(), "MaMonHoc", "TenMonHoc");
            ViewBag.PhongHoc = new SelectList(_phongHocService.GetAll(), "MaPhong");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LopHocPhan lopHocPhan)
        {
            // Note: Assuming validation is handled client-side or implicit strictness is low as per other controllers in snippet
            // If stricter validation is needed, we would check ModelState.IsValid.
            // Following pattern from PhongHocController snippet which just calls Add.
            
            try 
            {
                 _lopHocPhanService.Add(lopHocPhan);
                 return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Republish viewbags on failure if needed, but for now simple redirect or re-view
                ViewBag.GiangVien = new SelectList(_giangVienService.GetAll(), "MaGiangVien", "HoTen");
                ViewBag.HocKy = new SelectList(_hocKyService.GetAll(), "NamHoc", "HocKySo");
                ViewBag.MonHoc = new SelectList(_monHocService.GetAll(), "MaMonHoc", "TenMonHoc");
                ViewBag.PhongHoc = new SelectList(_phongHocService.GetAll(), "MaPhong");
                return View(lopHocPhan);
            }
        }

        public IActionResult Edit(string id)
        {
            var lopHocPhan = _lopHocPhanService.GetById(id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }

            ViewBag.GiangVien = new SelectList(_giangVienService.GetAll(), "MaGiangVien", "HoTen", lopHocPhan.MaGiangVien);
            ViewBag.HocKy = new SelectList(_hocKyService.GetAll(), "NamHoc", "HocKySo","MaHocKy", lopHocPhan.MaHocKy);
            ViewBag.MonHoc = new SelectList(_monHocService.GetAll(), "MaMonHoc", "TenMonHoc", lopHocPhan.MaMonHoc);
            ViewBag.PhongHoc = new SelectList(_phongHocService.GetAll(), "MaPhong", lopHocPhan.PhongHoc);
            return View(lopHocPhan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LopHocPhan lopHocPhan)
        {
             try
             {
                _lopHocPhanService.Update(lopHocPhan);
                return RedirectToAction(nameof(Index));
             }
             catch
             {
                ViewBag.GiangVien = new SelectList(_giangVienService.GetAll(), "MaGiangVien", "HoTen", lopHocPhan.MaGiangVien);
                ViewBag.HocKy = new SelectList(_hocKyService.GetAll(), "NamHoc", "HocKySo","MaHocKy", lopHocPhan.MaHocKy);
                ViewBag.MonHoc = new SelectList(_monHocService.GetAll(), "MaMonHoc", "TenMonHoc", lopHocPhan.MaMonHoc);
                ViewBag.PhongHoc = new SelectList(_phongHocService.GetAll(), "MaPhong", lopHocPhan.PhongHoc);
                return View(lopHocPhan);
             }
        }
    }
}
