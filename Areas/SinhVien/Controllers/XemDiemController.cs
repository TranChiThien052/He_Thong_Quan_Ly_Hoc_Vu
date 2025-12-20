using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;
using System.Security.Claims;

namespace QuanLyHocVu.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    [Authorize(Roles = "SinhVien")]
    public class XemDiemController : Controller
    {
        private readonly IDiemHocPhanService _diemHocPhanService;
        private readonly IHocKyService _hocKyService;

        public XemDiemController(IDiemHocPhanService diemHocPhanService, IHocKyService hocKyService)
        {
            _diemHocPhanService = diemHocPhanService;
            _hocKyService = hocKyService;
        }

        public IActionResult Index(string namHoc, int? hocKySo)
        {
            var maSinhVien = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (maSinhVien == null)
            {
                return NotFound();
            }

            var danhSachHocKy = _hocKyService.GetAll();

            if (string.IsNullOrEmpty(namHoc) || hocKySo == null)
            {
                var hocKyMoiNhat = _hocKyService.GetNewest();
                if (hocKyMoiNhat != null)
                {
                    namHoc = hocKyMoiNhat.NamHoc;
                    hocKySo = hocKyMoiNhat.HocKySo;
                }
            }

            var hocKyDuocChon = danhSachHocKy.FirstOrDefault(h => h.NamHoc == namHoc && h.HocKySo == hocKySo);
            
            var danhSachDiem = new List<DiemHocPhan>();
            if (hocKyDuocChon != null)
            {
                danhSachDiem = _diemHocPhanService.GetBySinhVienAndHocKy(maSinhVien, hocKyDuocChon.MaHocKy);
            }

            ViewBag.DanhSachNamHoc = danhSachHocKy.Select(h => h.NamHoc).Distinct().OrderByDescending(y => y).ToList();
            ViewBag.DanhSachHocKy = danhSachHocKy.Select(h => h.HocKySo).Distinct().OrderBy(s => s).ToList();
            ViewBag.NamHocDuocChon = namHoc;
            ViewBag.HocKyDuocChon = hocKySo;

            return View(danhSachDiem);
        }
    }
}
