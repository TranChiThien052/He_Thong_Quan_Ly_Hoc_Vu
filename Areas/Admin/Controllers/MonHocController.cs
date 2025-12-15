using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MonHocController : Controller
    {
        private readonly IMonHocService _service;

        public MonHocController(IMonHocService service)
        {
            _service = service;
        }

        public IActionResult Index(string loaiMon, string tenMonHoc)
        {
            // Admin thấy toàn bộ danh sách, có thể sau này thêm nút Sửa/Xóa
            var monHoc = _service.GetAll();
            if(!string.IsNullOrEmpty(loaiMon)){
                monHoc = monHoc.Where(m => m.LoaiMon == loaiMon).ToList();
            }

            if(!string.IsNullOrEmpty(tenMonHoc)){
                monHoc = monHoc.Where(m => m.TenMonHoc.Contains(tenMonHoc)).ToList();
            }

            var loaiMonList = _service.GetAll().Select(m => m.LoaiMon).Distinct().ToList();
            ViewBag.LoaiMon = new SelectList(loaiMonList);
            ViewBag.CurrentTenMonHoc = tenMonHoc;
            ViewBag.CurrentLoaiMon = loaiMon;

            return View(monHoc);
        }

        // public IActionResult Delete(string id){
        //     _service.Delete(id);
        //     return RedirectToAction("Index");
        // }

        public IActionResult Create(){
            var newId= GenerateNextId();
            ViewBag.Newid = newId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MonHoc monhoc)
        {
            _service.Add(monhoc);
            return RedirectToAction("Index");
        }
        private string GenerateNextId()
        {
            const string prefix = "MH";
            var existing = _service.GetAll()
                                   .Select(m => m.MaMonHoc)
                                   .Where(id => id.StartsWith(prefix))
                                   .Select(id => int.Parse(id.Substring(prefix.Length)))
                                   .ToList();
            int next = existing.Any() ? existing.Max() + 1 : 1;
            return prefix + next.ToString("D3");   // MH001, MH002, …
        }

        public IActionResult Edit(string id){
            var data = _service.GetById(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MonHoc monhoc){
            _service.Update(monhoc);
            return RedirectToAction("Index");
        }
    }
}
