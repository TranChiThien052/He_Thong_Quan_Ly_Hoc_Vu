using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyHocVu.Services;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DiemRenLuyenController : Controller
    {
        private readonly IDiemRenLuyenService _diemRenLuyenService;
        private readonly IHocKyService _hocKyService;

        public DiemRenLuyenController(IDiemRenLuyenService diemRenLuyenService, IHocKyService hocKyService)
        {
            _diemRenLuyenService = diemRenLuyenService;
            _hocKyService = hocKyService;
        }

        public IActionResult Index(string searchString, string maHocKy)
        {
            var hocKys = _hocKyService.GetAll().OrderByDescending(h => h.NgayKetThuc).ToList();
            var latestHocKy = hocKys.FirstOrDefault();

            if (string.IsNullOrEmpty(maHocKy))
            {
                maHocKy = latestHocKy?.MaHocKy;
            }

            ViewBag.MaHocKy = new SelectList(hocKys, "MaHocKy", "MaHocKy", maHocKy);

            var list = _diemRenLuyenService.GetByHocKy(maHocKy);
            
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                list = list.Where(d => d.MaSinhVien.ToLower().Contains(searchString) || 
                                       d.MaSinhVienNavigation.HoTen.ToLower().Contains(searchString)).ToList();
            }

            ViewBag.CurrentFilter = searchString;
            return View(list);
        }

        public IActionResult Edit(string maSinhVien, string maHocKy)
        {
            var diemRenLuyen = _diemRenLuyenService.GetById(maSinhVien, maHocKy);
            if (diemRenLuyen == null)
            {
                return NotFound();
            }
            return View(diemRenLuyen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DiemRenLuyen diemRenLuyen)
        {
            var existing = _diemRenLuyenService.GetById(diemRenLuyen.MaSinhVien, diemRenLuyen.MaHocKy);
            if (existing == null)
            {
                return NotFound();
            }
            _diemRenLuyenService.Update(diemRenLuyen.MaSinhVien, diemRenLuyen.MaHocKy, diemRenLuyen.Diem);
            return RedirectToAction("Index", new { maHocKy = diemRenLuyen.MaHocKy });
        }
    }
}
