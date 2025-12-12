using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Models;
using System.Security.Claims;

namespace QuanLyHocVu.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public AccountController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToRoleHome();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(TaiKhoan model)
        {
            // Bỏ qua lỗi validate của MaNguoiDung vì khi login chưa có thông tin này
            ModelState.Remove("MaNguoiDung");
            ModelState.Remove("MaNguoiDungNavigation");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1. Tìm tài khoản
            var taiKhoan = _context.TaiKhoans.FirstOrDefault(tk => tk.TenDangNhap == model.TenDangNhap && tk.MatKhau == model.MatKhau);

            if (taiKhoan == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                return View(model);
            }

            if (taiKhoan.TrangThai == "Khoa" || taiKhoan.TrangThai == "Locked") // Ví dụ kiểm tra trạng thái
            {
                ModelState.AddModelError("", "Tài khoản đã bị khóa");
                return View(model);
            }

            // 2. Xác định vai trò (Role)
            string role = "User";
            string maNguoiDung = taiKhoan.MaNguoiDung;
            string hoTen = _context.NguoiDungs.Find(maNguoiDung)?.HoTen ?? "User";

            // Kiểm tra xem ID này có nằm trong bảng SinhVien k?
            if (_context.SinhViens.Any(sv => sv.MaNguoiDung == maNguoiDung))
            {
                role = "SinhVien";
            }
            // Kiểm tra xem ID này có nằm trong bảng GiangVien k?
            else if (_context.GiangViens.Any(gv => gv.MaNguoiDung == maNguoiDung))
            {
                role = "GiangVien";
            }
            // Kiểm tra xem ID này có nằm trong bảng CanBo k?
            else if (_context.CanBos.Any(cb => cb.MaNguoiDung == maNguoiDung))
            {
                role = "Admin"; // Hoặc CanBo
            }

            // 3. Tạo Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, hoTen),
                new Claim(ClaimTypes.NameIdentifier, maNguoiDung),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Giữ đăng nhập
            };

            // 4. Sign In
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // 5. Điều hướng dựa trên Role
            return RedirectToRoleArea(role);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private IActionResult RedirectToRoleHome()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            return RedirectToRoleArea(role);
        }

        private IActionResult RedirectToRoleArea(string role)
        {
            switch (role)
            {
                case "SinhVien":
                    return RedirectToAction("Index", "Home", new { area = "SinhVien" });
                case "GiangVien":
                    // Giả sử bạn sẽ tạo Area GiangVien sau
                    return RedirectToAction("Index", "Home", new { area = "GiangVien" });
                case "Admin":
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                default:
                    return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
