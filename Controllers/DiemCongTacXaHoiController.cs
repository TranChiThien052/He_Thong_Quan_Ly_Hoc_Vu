using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Controllers
{
    public class DiemCongTacXaHoiController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public DiemCongTacXaHoiController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: DiemCongTacXaHoi
        public async Task<IActionResult> Index()
        {
            var quanLyHocVuContext = _context.DiemCongTacXaHois.Include(d => d.MaSinhVienNavigation);
            return View(await quanLyHocVuContext.ToListAsync());
        }

        // GET: DiemCongTacXaHoi/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diemCongTacXaHoi = await _context.DiemCongTacXaHois
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (diemCongTacXaHoi == null)
            {
                return NotFound();
            }

            return View(diemCongTacXaHoi);
        }

        // GET: DiemCongTacXaHoi/Create
        public IActionResult Create()
        {
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung");
            return View();
        }

        // POST: DiemCongTacXaHoi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSinhVien,TongDiem,GhiChu")] DiemCongTacXaHoi diemCongTacXaHoi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diemCongTacXaHoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", diemCongTacXaHoi.MaSinhVien);
            return View(diemCongTacXaHoi);
        }

        // GET: DiemCongTacXaHoi/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diemCongTacXaHoi = await _context.DiemCongTacXaHois.FindAsync(id);
            if (diemCongTacXaHoi == null)
            {
                return NotFound();
            }
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", diemCongTacXaHoi.MaSinhVien);
            return View(diemCongTacXaHoi);
        }

        // POST: DiemCongTacXaHoi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSinhVien,TongDiem,GhiChu")] DiemCongTacXaHoi diemCongTacXaHoi)
        {
            if (id != diemCongTacXaHoi.MaSinhVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diemCongTacXaHoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiemCongTacXaHoiExists(diemCongTacXaHoi.MaSinhVien))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", diemCongTacXaHoi.MaSinhVien);
            return View(diemCongTacXaHoi);
        }

        // GET: DiemCongTacXaHoi/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diemCongTacXaHoi = await _context.DiemCongTacXaHois
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (diemCongTacXaHoi == null)
            {
                return NotFound();
            }

            return View(diemCongTacXaHoi);
        }

        // POST: DiemCongTacXaHoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var diemCongTacXaHoi = await _context.DiemCongTacXaHois.FindAsync(id);
            if (diemCongTacXaHoi != null)
            {
                _context.DiemCongTacXaHois.Remove(diemCongTacXaHoi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiemCongTacXaHoiExists(string id)
        {
            return _context.DiemCongTacXaHois.Any(e => e.MaSinhVien == id);
        }
    }
}
