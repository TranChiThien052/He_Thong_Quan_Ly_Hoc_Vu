using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiKhoanController : Controller
    {
        private readonly QuanLyHocVuContext _context;
        private readonly Services.ISinhVienService _sinhVienService;
        private readonly Services.IGiangVienService _giangVienService;

        public TaiKhoanController(QuanLyHocVuContext context, Services.ISinhVienService sinhVienService, Services.IGiangVienService giangVienService)
        {
            _context = context;
            _sinhVienService = sinhVienService;
            _giangVienService = giangVienService;
        }

        // GET: Admin/TaiKhoan
        public IActionResult Index()
        {
            var taiKhoans = _context.TaiKhoans
                .Include(t => t.MaNguoiDungNavigation)
                .ToList();
            return View(taiKhoans);
        }

        // POST: Admin/TaiKhoan/CreateAccountBulk
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAccountBulk([FromForm] List<string> studentIds)
        {
            try
            {
                if (studentIds == null || studentIds.Count == 0)
                {
                    TempData["ErrorMessage"] = "Không có sinh viên nào được chọn!";
                    return RedirectToAction("Index", "SinhVien");
                }

                int successCount = 0;
                int errorCount = 0;

                foreach (var studentId in studentIds)
                {
                    try
                    {
                        var sinhVien = _sinhVienService.GetById(studentId);
                        if (sinhVien == null || sinhVien.TaiKhoan != null)
                        {
                            errorCount++;
                            continue;
                        }

                        var taiKhoan = new TaiKhoan
                        {
                            MaNguoiDung = sinhVien.MaNguoiDung,
                            TenDangNhap = sinhVien.MaNguoiDung,
                            MatKhau = sinhVien.MaNguoiDung,
                            TrangThai = "Hoạt động"
                        };

                        _context.TaiKhoans.Add(taiKhoan);
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        // Log error if needed
                    }
                }

                _context.SaveChanges();
                
                TempData["SuccessMessage"] = $"Tạo thành công {successCount} tài khoản!";
                if (errorCount > 0)
                {
                    TempData["WarningMessage"] = $"Có {errorCount} sinh viên không thể tạo tài khoản (đã có tài khoản hoặc không tồn tại).";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi lưu dữ liệu: {ex.Message}";
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
                if (giangVienIds == null || giangVienIds.Count == 0)
                {
                    TempData["ErrorMessage"] = "Không có giảng viên nào được chọn!";
                    return RedirectToAction("Index", "GiangVien");
                }

                int successCount = 0;
                int errorCount = 0;

                foreach (var id in giangVienIds)
                {
                    try
                    {
                        var giangVien = _giangVienService.GetById(id);
                        if (giangVien == null || giangVien.TaiKhoan != null)
                        {
                            errorCount++;
                            continue;
                        }

                        var taiKhoan = new TaiKhoan
                        {
                            MaNguoiDung = giangVien.MaNguoiDung,
                            TenDangNhap = giangVien.MaNguoiDung,
                            MatKhau = giangVien.MaNguoiDung,
                            TrangThai = "Hoạt động"
                        };

                        _context.TaiKhoans.Add(taiKhoan);
                        successCount++;
                    }
                    catch (Exception)
                    {
                        errorCount++;
                    }
                }

                _context.SaveChanges();

                TempData["SuccessMessage"] = $"Tạo thành công {successCount} tài khoản giảng viên!";
                if (errorCount > 0)
                {
                    TempData["WarningMessage"] = $"Có {errorCount} giảng viên không thể tạo tài khoản (đã có tài khoản hoặc không tồn tại).";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi lưu dữ liệu: {ex.Message}";
            }

            return RedirectToAction("Index", "GiangVien");
        }

        // GET: Admin/TaiKhoan/Edit/5
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var taiKhoan = _context.TaiKhoans
                .Include(t => t.MaNguoiDungNavigation)
                .FirstOrDefault(t => t.MaNguoiDung == id);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoan/Edit/5
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
                    _context.Update(taiKhoan);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Cập nhật tài khoản thành công!";
                    
                    // Determine where to redirect based on user role (prefix check)
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
                    else
                    {
                        throw;
                    }
                }
            }
            return View(taiKhoan);
        }

        private bool TaiKhoanExists(string id)
        {
            return _context.TaiKhoans.Any(e => e.MaNguoiDung == id);
        }
    }
}
