using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PhongHocController : Controller
    {
        private readonly IPhongHocService _phongHocService;

        public PhongHocController(IPhongHocService phongHocService)
        {
            _phongHocService = phongHocService;
        }

        public IActionResult Index(string loaiPhong, string tang)
        {
            var phongHoc = _phongHocService.GetAll();
            if(!string.IsNullOrEmpty(loaiPhong)){
                phongHoc = phongHoc.Where(p => p.LoaiPhong == loaiPhong).ToList();
            }
            if(!string.IsNullOrEmpty(tang)){
                phongHoc = phongHoc.Where(p => p.Tang.ToString() == tang).ToList();
            }
            var loaiPhongList = _phongHocService.GetAll().Select(p => p.LoaiPhong).Distinct().ToList();
            var tangList = _phongHocService.GetAll().Select(p => p.Tang).Distinct().ToList();
            ViewBag.LoaiPhong = new SelectList(loaiPhongList);
            ViewBag.Tang=new SelectList(tangList);
            
            return View(phongHoc);
        }

        public IActionResult Create(){
            return View();
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhongHoc phongHoc){
            _phongHocService.Add(phongHoc);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id){
            var phongHoc = _phongHocService.GetById(id);
            return View(phongHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PhongHoc phongHoc){
            _phongHocService.Update(phongHoc);
            return RedirectToAction("Index");
        }
    }
}