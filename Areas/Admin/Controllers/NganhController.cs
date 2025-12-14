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
        private readonly IKhoaNganhService _khoaNganhService;
        public NganhController(IKhoaService khoaService, INganhService nganhService, IKhoaNganhService khoaNganhService)
        {
            _khoaService = khoaService;
            _nganhService = nganhService;
            _khoaNganhService = khoaNganhService;
        }
        public IActionResult Create()
        {
            ViewBag.Khoas = _khoaService.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MaNganh,TenNganh")] Nganh nganh, List<string> khoa)
        {
            _nganhService.Add(nganh);

            foreach (var maKhoa in khoa)
            {
                var khoaExisted = _khoaService.GetById(maKhoa);
                if (khoaExisted != null)
                {
                    var khoaNganh = new KhoaNganh
                    {
                        MaKhoa = khoaExisted.MaKhoa,
                        MaNganh = nganh.MaNganh
                    };

                    _khoaNganhService.Add(khoaNganh);
                }
            }

            return RedirectToAction("Index", "KhoaNganh");
        }
        public IActionResult Edit(string id)
        {   
            var nganh = _nganhService.GetById(id);
            ViewBag.Khoas = _khoaService.GetAll();
            return View(nganh);
        }
    }
}