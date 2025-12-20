using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    [Authorize(Roles = "SinhVien")]
    public class DangKyHocPhanController : Controller
    {
        private readonly ILopHocPhanService _lopHocPhanService;
        private readonly IHocKyService _hocKyService;
        private readonly ISinhVienService _sinhVienService;
        private readonly IDangKyHocPhanService _dangKyHocPhanService;

        public DangKyHocPhanController(
            ILopHocPhanService lopHocPhanService, 
            IHocKyService hocKyService,
            ISinhVienService sinhVienService,
            IDangKyHocPhanService dangKyHocPhanService)
        {
            _lopHocPhanService = lopHocPhanService;
            _hocKyService = hocKyService;
            _sinhVienService = sinhVienService;
            _dangKyHocPhanService = dangKyHocPhanService;
        }

        public IActionResult Index()
        {
            var maSinhVien = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var sinhVien = _sinhVienService.GetById(maSinhVien);
            var hocKy = _hocKyService.GetNewest();

            if (sinhVien == null || hocKy == null)
            {
                return NotFound();
            }

            var lopHocPhanDaDangKys = _dangKyHocPhanService.GetBySinhVien(maSinhVien)
                                        .Where(d => d.MaLopHocPhanNavigation.MaHocKy == hocKy.MaHocKy)
                                        .Select(d => d.MaLopHocPhanNavigation)
                                        .ToList();

            var maMonHocDaDangKys = lopHocPhanDaDangKys.Select(l => l.MaMonHoc).ToList();

            var lopHocPhans = _lopHocPhanService.GetByHocKyAndNganh(hocKy.MaHocKy, sinhVien.MaNganh)
                                .Where(l => !maMonHocDaDangKys.Contains(l.MaMonHoc))
                                .ToList();
            
            ViewBag.LopHocPhans = lopHocPhans;
            ViewBag.HocKy = hocKy;
            ViewBag.LopHocPhanDaDangKys = lopHocPhanDaDangKys;
            
            return View();
        }

        [HttpPost]
        public IActionResult Add(string maLopHocPhan)
        {
            var maSinhVien = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var dangKyHocPhan = new DangKyHocPhan { MaSinhVien = maSinhVien, MaLopHocPhan = maLopHocPhan};
            
            _dangKyHocPhanService.Add(dangKyHocPhan);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(String MaLopHocPhan) 
        {
            var maSinhVien = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            _dangKyHocPhanService.Delete(maSinhVien, MaLopHocPhan);
            return RedirectToAction("Index");
        }
    }
}