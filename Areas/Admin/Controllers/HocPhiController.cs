using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HocPhiController : Controller
    {
        private readonly IHocPhiService _hocPhiService;

        public HocPhiController(IHocPhiService hocPhiService)
        {
            _hocPhiService = hocPhiService;
        }

        public IActionResult Index()
        {
            var HocPhis = _hocPhiService.GetAll();
            return View(HocPhis);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MaHocPhi,GiaTheoTin")]HocPhi hocPhi)
        {
            _hocPhiService.Add(hocPhi);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var hocPhi = _hocPhiService.GetById(id);
            return View(hocPhi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("MaHocPhi,GiaTheoTin")] HocPhi hocPhi)
        {
            _hocPhiService.Update(hocPhi);
            return RedirectToAction("Index");
        }
    }
}