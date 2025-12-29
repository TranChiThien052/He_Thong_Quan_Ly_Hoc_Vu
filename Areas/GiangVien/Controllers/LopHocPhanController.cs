using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Services;
using QuanLyHocVu.Models;
namespace QuanLyHocVu.Areas.GiangVien.Controllers

{
    [Area("GiangVien")]
    [Authorize(Roles = "GiangVien")]
    public class LopHocPhanController : Controller
    {
        private readonly ILopHocPhanService _lopHocPhanService;
        private readonly IGiangVienService _giangVienService;
        private readonly IDiemHocPhanService _diemHocPhanService;

        public LopHocPhanController(ILopHocPhanService lopHocPhanService, IGiangVienService giangVienService, IDiemHocPhanService diemHocPhanService)
        {
            _lopHocPhanService = lopHocPhanService;
            _giangVienService = giangVienService;
            _diemHocPhanService = diemHocPhanService;
        }



        public IActionResult Index()
        {
            var maGiangVienClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string maGiangVien = maGiangVienClaim.Value;
            var lopHocPhan = _lopHocPhanService.GetAll().Where(l => l.MaGiangVien == maGiangVien).ToList();
            if(lopHocPhan == null){
                return NotFound();
            }
            return View(lopHocPhan);
        }

        public IActionResult Details(string id)
        {
            var lopHocPhan = _lopHocPhanService.GetById(id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }

            var diemHocPhans = _diemHocPhanService.GetByLopHocPhan(id);
            ViewBag.DiemList = diemHocPhans;

            return View(lopHocPhan);
        }

        public IActionResult NhapDiem(string maSinhVien, string maLopHocPhan){
            var diemHocPhan = _diemHocPhanService.GetById(maSinhVien, maLopHocPhan);
            ViewBag.maLopHocPhan = maLopHocPhan;
            if(diemHocPhan == null){
                return NotFound();
            }
            return View(diemHocPhan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NhapDiem(DiemHocPhan diemHocPhan){
            _diemHocPhanService.UpdateDiem(diemHocPhan);
            return RedirectToAction("Details",new {id = diemHocPhan.MaLopHocPhan});
        }

    }
}