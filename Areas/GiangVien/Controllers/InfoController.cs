using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Services;
using SQLitePCL;

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

        public IActionResult Edit(string id){
            if(id == null){
                return NotFound();
            }
            var giangVien = _giangVienService.GetById(id);
            if(giangVien == null){
                return NotFound();
            }
            return View(giangVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, QuanLyHocVu.Models.GiangVien giangVien)
        {
            ModelState.Remove("MaKhoaNavigation");
            if(id != giangVien.MaNguoiDung){
                return NotFound();
            }
            else{
                _giangVienService.Update(giangVien);
                var identity = (ClaimsIdentity)User.Identity;
                    var claims = identity.Claims.ToList();
                    var tenCu = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    if (tenCu != null)
                    {
                        identity.RemoveClaim(tenCu);
                    }
                    identity.AddClaim(new Claim(ClaimTypes.Name, giangVien.HoTen));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { IsPersistent = true }
                    );
                return RedirectToAction(nameof(Index));
            }

        }
    }
}