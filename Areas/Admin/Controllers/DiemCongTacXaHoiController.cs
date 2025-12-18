using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Services;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DiemCongTacXaHoiController : Controller
    {
        private readonly IDiemCongTacXaHoiService _diemCongTacXaHoiService;

        public DiemCongTacXaHoiController(IDiemCongTacXaHoiService diemCongTacXaHoiService)
        {
            _diemCongTacXaHoiService = diemCongTacXaHoiService;
        }

        public IActionResult Index(string searchString)
        {
            var list = _diemCongTacXaHoiService.Search(searchString);
            ViewBag.CurrentFilter = searchString;
            return View(list);
        }

        public IActionResult Edit(string maSinhVien)
        {
            var diemCongTacXaHoi = _diemCongTacXaHoiService.GetBySinhVien(maSinhVien);
            if (diemCongTacXaHoi == null)
            {
                return NotFound();
            }
            return View(diemCongTacXaHoi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DiemCongTacXaHoi diemCongTacXaHoi)
        {
            _diemCongTacXaHoiService.Update(diemCongTacXaHoi.MaSinhVien, diemCongTacXaHoi.TongDiem);
            return RedirectToAction(nameof(Index));
        }
    }
}
