using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Services;

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

        [HttpPost]
        public IActionResult Edit(string maSinhVien, int? tongDiem)
        {
            _diemCongTacXaHoiService.Update(maSinhVien, tongDiem);
            return RedirectToAction(nameof(Index));
        }
    }
}
