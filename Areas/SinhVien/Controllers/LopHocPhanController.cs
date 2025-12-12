using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    public class LopHocPhanController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public LopHocPhanController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: SinhVien/LopHocPhan
        public async Task<IActionResult> Index()
        {
            var quanLyHocVuContext = _context.LopHocPhans.Include(l => l.MaGiangVienNavigation).Include(l => l.MaHocKyNavigation).Include(l => l.MaMonHocNavigation).Include(l => l.PhongHocNavigation);
            return View(await quanLyHocVuContext.ToListAsync());
        }

        // GET: SinhVien/LopHocPhan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lopHocPhan = await _context.LopHocPhans
                .Include(l => l.MaGiangVienNavigation)
                .Include(l => l.MaHocKyNavigation)
                .Include(l => l.MaMonHocNavigation)
                .Include(l => l.PhongHocNavigation)
                .FirstOrDefaultAsync(m => m.MaLopHocPhan == id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }

            return View(lopHocPhan);
        }
    }
}