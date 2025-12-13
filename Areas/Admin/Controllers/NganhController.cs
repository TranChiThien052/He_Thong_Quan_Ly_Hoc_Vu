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
        private readonly QuanLyHocVuContext _context;
        private readonly IKhoaService _khoaService;
        private readonly INganhService _nganhService;
        public NganhController(QuanLyHocVuContext context, IKhoaService khoaService, INganhService nganhService)
        {
            _context = context;
            _khoaService = khoaService;
            _nganhService = nganhService;
        }
        public IActionResult Create()
        {
            ViewBag.Khoas = _khoaService.GetAll();
            return View();
        }
    }
}