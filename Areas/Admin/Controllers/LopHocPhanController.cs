using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;
using System.Text.Json;

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
            var newId = GenerateLopHocPhanId();
            ViewBag.MaLopHocPhan = newId;

            ViewBag.GiangVienJson = JsonSerializer.Serialize(_giangVienService.GetAll().Select(g => new { g.MaNguoiDung, g.HoTen }));
            ViewBag.HocKyJson = JsonSerializer.Serialize(_hocKyService.GetAll().Select(h => new { h.MaHocKy, h.NamHoc, h.HocKySo }));
            ViewBag.MonHocJson = JsonSerializer.Serialize(_monHocService.GetAll().Select(m => new { m.MaMonHoc, m.TenMonHoc }));

            ViewBag.PhongHoc = _phongHocService.GetAll();
            return View();
        }

        private string GenerateLopHocPhanId(){
            const string prefix = "LHP";
            var existingIds = _lopHocPhanService.GetAll()
            .Select(l=>l.MaLopHocPhan)
            .Where(id => id.StartsWith(prefix))
            .Select(id => int.Parse(id.Substring(prefix.Length)))
            .ToList();
            int nextId = existingIds.Any() ? existingIds.Max() + 1 : 1;
            return prefix + nextId.ToString("D5");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LopHocPhan lopHocPhan)
        {
            try 
            {
                _lopHocPhanService.Add(lopHocPhan);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.GiangVienJson = JsonSerializer.Serialize(_giangVienService.GetAll().Select(g => new { g.MaNguoiDung, g.HoTen }));
                ViewBag.HocKyJson = JsonSerializer.Serialize(_hocKyService.GetAll().Select(h => new { h.MaHocKy, h.NamHoc, h.HocKySo }));
                ViewBag.MonHocJson = JsonSerializer.Serialize(_monHocService.GetAll().Select(m => new { m.MaMonHoc, m.TenMonHoc }));
                ViewBag.PhongHoc = _phongHocService.GetAll();
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
