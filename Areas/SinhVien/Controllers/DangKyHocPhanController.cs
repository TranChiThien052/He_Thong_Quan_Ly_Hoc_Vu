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
        private readonly IDiemHocPhanService _diemHocPhanService;
        public DangKyHocPhanController(ILopHocPhanService lopHocPhanService, IHocKyService hocKyService, ISinhVienService sinhVienService, IDangKyHocPhanService dangKyHocPhanService, IDiemHocPhanService diemHocPhanService)
        {
            _lopHocPhanService = lopHocPhanService;
            _hocKyService = hocKyService;
            _sinhVienService = sinhVienService;
            _dangKyHocPhanService = dangKyHocPhanService;
            _diemHocPhanService = diemHocPhanService;
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
            
            var diemHocPhan = new DiemHocPhan { MaSinhVien = maSinhVien, MaLopHocPhan = maLopHocPhan, DiemChuyenCan = 0, DiemGiuaKy = 0, DiemCuoiKy = 0};

            _dangKyHocPhanService.Add(dangKyHocPhan);
            _diemHocPhanService.Add(diemHocPhan);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string MaLopHocPhan) 
        {
            var maSinhVien = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            _dangKyHocPhanService.Delete(maSinhVien, MaLopHocPhan);
            _diemHocPhanService.Delete(maSinhVien, MaLopHocPhan);
            return RedirectToAction("Index");
        }
    }
}