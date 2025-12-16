using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HocKyController : Controller
    {
        private readonly IHocKyService _hocKyService;
        public HocKyController(IHocKyService hocKyService)
        {
            _hocKyService = hocKyService;
        }
        public IActionResult Index(string maHocKy, string namHoc)
        {
            var hocKys = _hocKyService.GetAll();
            
            if(!string.IsNullOrEmpty(maHocKy)){
                hocKys = hocKys.Where(m => m.MaHocKy == maHocKy).ToList();
            }

            if(!string.IsNullOrEmpty(namHoc)){
                hocKys = hocKys.Where(m => m.NamHoc == namHoc).ToList();
            }

            return View(hocKys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MaHocKy,NamHoc,HocKySo,NgayBatDau,NgayKetThuc")] HocKy hocKy)
        {
            hocKy.MaHocKy = hocKy.NamHoc + "-" + hocKy.HocKySo;
            _hocKyService.Add(hocKy);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id) {
            var HocKy = _hocKyService.GetById(id);
            return View(HocKy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("MaHocKy,NamHoc,HocKySo,NgayBatDau,NgayKetThuc")] HocKy hocKy)
        {
            _hocKyService.Update(hocKy);
            return RedirectToAction("Index");
        }
    }
}