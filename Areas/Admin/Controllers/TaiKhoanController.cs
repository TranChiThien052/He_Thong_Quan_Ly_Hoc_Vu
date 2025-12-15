using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TaiKhoanController : Controller
    {
        private readonly ISinhVienService _sinhVienService;
        private readonly IGiangVienService _giangVienService;
        private readonly ITaiKhoanService _taiKhoanServices;

        public TaiKhoanController(ISinhVienService sinhVienService, IGiangVienService giangVienService, ITaiKhoanService taiKhoanService)
        {
            _sinhVienService = sinhVienService;
            _giangVienService = giangVienService;
            _taiKhoanServices = taiKhoanService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccountBulkForSinhVien([FromForm] List<string> studentIds)
        {
            try
            {
                if (studentIds == null || studentIds.Count == 0)
                {
                    TempData["ErrorMessage"] = "Không có sinh viên nào được chọn!";
                    return RedirectToAction("Index", "SinhVien");
                }

                foreach (var studentId in studentIds)
                {
                    try
                    {
                        var sinhVien = _sinhVienService.GetById(studentId);
                        if (sinhVien == null || sinhVien.TaiKhoan != null)
                        {
                            continue;
                        }

                        var taiKhoan = new TaiKhoan
                        {
                            MaNguoiDung = sinhVien.MaNguoiDung,
                            TenDangNhap = sinhVien.MaNguoiDung,
                            MatKhau = sinhVien.MaNguoiDung,
                            TrangThai = "Hoạt động"
                        };

                        _taiKhoanServices.Add(taiKhoan);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index", "SinhVien");
        }

        // POST: Admin/TaiKhoan/CreateAccountBulkForGiangVien
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccountBulkForGiangVien([FromForm] List<string> giangVienIds)
        {
            try
            {
                foreach (var id in giangVienIds)
                {
                    try
                    {
                        var giangVien = _giangVienService.GetById(id);
                        if (giangVien == null || giangVien.TaiKhoan != null)
                        {
                            continue;
                        }

                        var taiKhoan = new TaiKhoan
                        {
                            MaNguoiDung = giangVien.MaNguoiDung,
                            TenDangNhap = giangVien.MaNguoiDung,
                            MatKhau = giangVien.MaNguoiDung,
                            TrangThai = "Hoạt động"
                        };

                        _taiKhoanServices.Add(taiKhoan);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index", "GiangVien");
        }

        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var taiKhoan = _taiKhoanServices.GetById(id);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("MaNguoiDung,TenDangNhap,MatKhau,TrangThai")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.MaNguoiDung)
            {
                return NotFound();
            }

            ModelState.Remove("MaNguoiDungNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _taiKhoanServices.Update(taiKhoan);
                    string referer = Request.Headers["Referer"].ToString();
                    if (referer.Contains("GiangVien"))
                    {
                         return RedirectToAction("Index", "GiangVien");
                    }
                    return RedirectToAction("Index", "SinhVien");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.MaNguoiDung))
                    {
                        return NotFound();
                    }
                }
            }
            return View(taiKhoan);
        }

        private bool TaiKhoanExists(string id)
        {
            return _taiKhoanServices.Exists(id);
        }
    }
}
