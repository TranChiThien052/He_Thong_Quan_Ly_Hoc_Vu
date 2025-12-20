using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    [Authorize(Roles = "SinhVien")]
    public class LichHocController : Controller
    {
        private readonly IDangKyHocPhanService _dangKyHocPhanService;
        private readonly IHocKyService _hocKyService;

        public LichHocController(IDangKyHocPhanService dangKyHocPhanService, IHocKyService hocKyService)
        {
            _dangKyHocPhanService = dangKyHocPhanService;
            _hocKyService = hocKyService;
        }

        public IActionResult Index(string maHocKy = null)
        {
            var maSinhVien = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var hocKys = _hocKyService.GetAll();
            var dangKyHocPhans = _dangKyHocPhanService.GetBySinhVien(maSinhVien);

            if (string.IsNullOrEmpty(maHocKy))
            {
                var latestHocKy = hocKys.OrderByDescending(h => h.MaHocKy).FirstOrDefault();
                maHocKy = latestHocKy?.MaHocKy;
            } 
            else 
            {
                dangKyHocPhans = dangKyHocPhans.Where(d => d.MaLopHocPhanNavigation.MaHocKy == maHocKy).ToList();
            }

            ViewBag.HocKys = hocKys;
            ViewBag.SelectedHocKy = maHocKy;

            return View(dangKyHocPhans);
        }
    }
}