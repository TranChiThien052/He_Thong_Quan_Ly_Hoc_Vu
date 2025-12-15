using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Edit(string id, QuanLyHocVu.Models.GiangVien giangVien)
        {
            ModelState.Remove("MaKhoaNavigation");
            if(id != giangVien.MaNguoiDung){
                return NotFound();
            }
            else{
                _giangVienService.Update(giangVien);
                return RedirectToAction(nameof(Index));
            }
            

        }
    }
}