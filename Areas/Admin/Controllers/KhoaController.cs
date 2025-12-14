using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhoaController : Controller
    {
        private readonly QuanLyHocVuContext _context;
        private readonly IKhoaService _service;
        public KhoaController(QuanLyHocVuContext context, IKhoaService service)
        {
            _context = context;
            _service = service;
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MaKhoa,TenKhoa")] Khoa khoa)
        {
            _service.Add(khoa);
            return RedirectToAction("Index", "KhoaNganh");
        }

        public IActionResult Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var khoa = _service.GetById(id);
            if(khoa == null)
            {
                return NotFound();
            }
            return View(khoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("MaKhoa,TenKhoa")] Khoa khoa)
        {
            _service.Update(khoa);
            return RedirectToAction("Index", "KhoaNganh");
        }
    }
}

