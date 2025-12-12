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
    public class SinhVienHoatDongCtxhController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public SinhVienHoatDongCtxhController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: SinhVienHoatDongCtxh
        public async Task<IActionResult> Index()
        {
            var quanLyHocVuContext = _context.SinhVienHoatDongCtxhs.Include(s => s.MaHoatDongNavigation).Include(s => s.MaSinhVienNavigation);
            return View(await quanLyHocVuContext.ToListAsync());
        }

        // GET: SinhVienHoatDongCtxh/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVienHoatDongCtxh = await _context.SinhVienHoatDongCtxhs
                .Include(s => s.MaHoatDongNavigation)
                .Include(s => s.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (sinhVienHoatDongCtxh == null)
            {
                return NotFound();
            }

            return View(sinhVienHoatDongCtxh);
        }

        // GET: SinhVienHoatDongCtxh/Create
        public IActionResult Create()
        {
            ViewData["MaHoatDong"] = new SelectList(_context.HoatDongCtxhs, "MaHoatDong", "MaHoatDong");
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung");
            return View();
        }

        // POST: SinhVienHoatDongCtxh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSinhVien,MaHoatDong,NgayThamGia,DiemThucTe,GhiChu")] SinhVienHoatDongCtxh sinhVienHoatDongCtxh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhVienHoatDongCtxh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHoatDong"] = new SelectList(_context.HoatDongCtxhs, "MaHoatDong", "MaHoatDong", sinhVienHoatDongCtxh.MaHoatDong);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", sinhVienHoatDongCtxh.MaSinhVien);
            return View(sinhVienHoatDongCtxh);
        }

        // GET: SinhVienHoatDongCtxh/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVienHoatDongCtxh = await _context.SinhVienHoatDongCtxhs.FindAsync(id);
            if (sinhVienHoatDongCtxh == null)
            {
                return NotFound();
            }
            ViewData["MaHoatDong"] = new SelectList(_context.HoatDongCtxhs, "MaHoatDong", "MaHoatDong", sinhVienHoatDongCtxh.MaHoatDong);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", sinhVienHoatDongCtxh.MaSinhVien);
            return View(sinhVienHoatDongCtxh);
        }

        // POST: SinhVienHoatDongCtxh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSinhVien,MaHoatDong,NgayThamGia,DiemThucTe,GhiChu")] SinhVienHoatDongCtxh sinhVienHoatDongCtxh)
        {
            if (id != sinhVienHoatDongCtxh.MaSinhVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhVienHoatDongCtxh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienHoatDongCtxhExists(sinhVienHoatDongCtxh.MaSinhVien))
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
            ViewData["MaHoatDong"] = new SelectList(_context.HoatDongCtxhs, "MaHoatDong", "MaHoatDong", sinhVienHoatDongCtxh.MaHoatDong);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", sinhVienHoatDongCtxh.MaSinhVien);
            return View(sinhVienHoatDongCtxh);
        }

        // GET: SinhVienHoatDongCtxh/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVienHoatDongCtxh = await _context.SinhVienHoatDongCtxhs
                .Include(s => s.MaHoatDongNavigation)
                .Include(s => s.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (sinhVienHoatDongCtxh == null)
            {
                return NotFound();
            }

            return View(sinhVienHoatDongCtxh);
        }

        // POST: SinhVienHoatDongCtxh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhVienHoatDongCtxh = await _context.SinhVienHoatDongCtxhs.FindAsync(id);
            if (sinhVienHoatDongCtxh != null)
            {
                _context.SinhVienHoatDongCtxhs.Remove(sinhVienHoatDongCtxh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinhVienHoatDongCtxhExists(string id)
        {
            return _context.SinhVienHoatDongCtxhs.Any(e => e.MaSinhVien == id);
        }
    }
}
