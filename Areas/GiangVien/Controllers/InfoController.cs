using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.GiangVien.Controllers
{
    [Area("GiangVien")]
    [Authorize(Roles = "GiangVien")]
    public class InfoController : Controller
    {
        private readonly IGiangVienService _giangVienService;

        public InfoController(IGiangVienService giangVienService)
        {
            _giangVienService = giangVienService;
        }

        public IActionResult Index()
        {
            var maGiangVienClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string maGiangVien = maGiangVienClaim.Value;
            Models.GiangVien giangVien = _giangVienService.GetById(maGiangVien);
            if (giangVien == null)
            {
                return NotFound();
            }
            return View(giangVien);
        }

        public IActionResult Edit()
        {
            var maGiangVienClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string maGiangVien = maGiangVienClaim.Value;
            Models.GiangVien giangVien = _giangVienService.GetById(maGiangVien);
            if (giangVien == null)
            {
                return NotFound();
            }
            return View(giangVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("MaNguoiDung,HoTen,QueQuan,NgaySinh,Email,SoDienThoai,Cccd,DiaChiThuongTru,DiaChiTamTru,ChuyenMon,TinhTrangCongTac")] Models.GiangVien giangVien)
        {
            if (id != giangVien.MaNguoiDung)
            {
                return NotFound();
            }

            ModelState.Remove("MaKhoaNavigation");
            ModelState.Remove("TaiKhoan");

            if (ModelState.IsValid)
            {
                try
                {
                    _giangVienService.Update(giangVien);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_giangVienService.GetById(giangVien.MaNguoiDung) == null)
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
            return View(giangVien);
        }
    }
}