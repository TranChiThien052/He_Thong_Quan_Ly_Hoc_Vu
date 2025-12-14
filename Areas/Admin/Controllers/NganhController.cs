using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NganhController : Controller
    {
        private readonly IKhoaService _khoaService;
        private readonly INganhService _nganhService;
        public NganhController(IKhoaService khoaService, INganhService nganhService)
        {
            _khoaService = khoaService;
            _nganhService = nganhService;
        }
        public IActionResult Create()
        {
            ViewBag.Khoas = _khoaService.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MaNganh,TenNganh")] Nganh nganh, List<string> maKhoas)
        {
            foreach (var maKhoa in maKhoas)
            {
                var khoa = _khoaService.GetById(maKhoa);
                if (khoa != null)
                {
                    nganh.MaKhoas.Add(khoa);
                }
            }
            _nganhService.Add(nganh);
            return RedirectToAction("Index", "KhoaNganh");
        }
    }
}