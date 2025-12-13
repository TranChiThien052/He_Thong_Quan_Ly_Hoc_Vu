using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MonHocController : Controller
    {
        private readonly IMonHocService _service;

        public MonHocController(IMonHocService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            // Admin thấy toàn bộ danh sách, có thể sau này thêm nút Sửa/Xóa
            var data = _service.GetAll();
            return View(data);
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
    }
}
