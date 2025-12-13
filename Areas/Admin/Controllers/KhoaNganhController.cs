using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class KhoaNganhController : Controller
    {
        private readonly QuanLyHocVuContext _context;
        private readonly IKhoaService _khoaService;
        private readonly INganhService _nganhService;

        public KhoaNganhController(QuanLyHocVuContext context, IKhoaService khoaService, INganhService nganhService)
        {
            _context = context;
            _khoaService = khoaService;
            _nganhService = nganhService;
        }

        public IActionResult Index()
        {
            var khoas = _khoaService.GetAll();
            var nganhs = _nganhService.GetAll();
            ViewBag.Khoas = khoas;
            ViewBag.Nganhs = nganhs;
            return View();
        }
    }
}