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
            ModelState.Remove("MaNguoiDung");
            ModelState.Remove("MaNguoiDungNavigation");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var taiKhoan = _context.TaiKhoans.FirstOrDefault(tk => tk.TenDangNhap == model.TenDangNhap && tk.MatKhau == model.MatKhau);

            if (taiKhoan == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                return View(model);
            }

            if (taiKhoan.TrangThai == "Khóa")
            {
                ModelState.AddModelError("", "Tài khoản đã bị khóa");
                return View(model);
            }

            string role = "User";
            string maNguoiDung = taiKhoan.MaNguoiDung;
            string hoTen = _context.NguoiDungs.Find(maNguoiDung)?.HoTen ?? "User";

            if (_context.SinhViens.Any(sv => sv.MaNguoiDung == maNguoiDung))
            {
                role = "SinhVien";
            }
            else if (_context.GiangViens.Any(gv => gv.MaNguoiDung == maNguoiDung))
            {
                role = "GiangVien";
            }
            else if (_context.CanBos.Any(cb => cb.MaNguoiDung == maNguoiDung))
            {
                role = "Admin";
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, hoTen),
                new Claim(ClaimTypes.NameIdentifier, maNguoiDung),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

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
                    return RedirectToAction("Index", "Home", new { area = "GiangVien" });
                case "Admin":
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                default:
                    return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
