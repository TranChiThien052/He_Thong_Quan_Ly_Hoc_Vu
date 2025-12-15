using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Services;
using SQLitePCL;

namespace QuanLyHocVu.Areas.SinhVien
{
    [Area("SinhVien")]
    [Authorize(Roles = "SinhVien")]
    public class InfoController : Controller
    {
        private readonly ISinhVienService _sinhVienService;

        public InfoController(ISinhVienService sinhVienService)
        {
            _sinhVienService = sinhVienService;
        }

        public IActionResult Index()
        {
            var maSinhVienClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string maSinhVien = maSinhVienClaim.Value;
            var sinhVien = _sinhVienService.GetById(maSinhVien);
            if(sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        public IActionResult Edit()
        {
            var maSinhVienClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string maSinhVien = maSinhVienClaim.Value;
            var sinhVien = _sinhVienService.GetById(maSinhVien);
            if(sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MaNguoiDung,HoTen,QueQuan,NgaySinh,Email,SoDienThoai,Cccd,DiaChiThuongTru,DiaChiTamTru,MaNganh,NienKhoa,TinhTrangHoc")] Models.SinhVien sinhVien)
        {
            ModelState.Remove("MaNganhNavigation");
            ModelState.Remove("TaiKhoan");

            if (ModelState.IsValid)
            {
                try
                {
                    _sinhVienService.Update(sinhVien);
                    var identity = (ClaimsIdentity)User.Identity;
                    var claims = identity.Claims.ToList();
                    var tenCu = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    if (tenCu != null)
                    {
                        identity.RemoveClaim(tenCu);
                    }
                    identity.AddClaim(new Claim(ClaimTypes.Name, sinhVien.HoTen));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { IsPersistent = true }
                    );
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_sinhVienService.GetById(sinhVien.MaNguoiDung) == null)
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
            return View(sinhVien);
        }
    }
}