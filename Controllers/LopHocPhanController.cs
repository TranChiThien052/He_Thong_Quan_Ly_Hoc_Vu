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
    public class LopHocPhanController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public LopHocPhanController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: LopHocPhan
        public async Task<IActionResult> Index()
        {
            var quanLyHocVuContext = _context.LopHocPhans.Include(l => l.MaGiangVienNavigation).Include(l => l.MaHocKyNavigation).Include(l => l.MaMonHocNavigation).Include(l => l.PhongHocNavigation);
            return View(await quanLyHocVuContext.ToListAsync());
        }

        // GET: LopHocPhan/Details/5
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

        // GET: LopHocPhan/Create
        public IActionResult Create()
        {
            ViewData["MaGiangVien"] = new SelectList(_context.GiangViens, "MaNguoiDung", "MaNguoiDung");
            ViewData["MaHocKy"] = new SelectList(_context.HocKies, "MaHocKy", "MaHocKy");
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "MaMonHoc");
            ViewData["PhongHoc"] = new SelectList(_context.PhongHocs, "MaPhong", "MaPhong");
            return View();
        }

        // POST: LopHocPhan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLopHocPhan,MaMonHoc,MaGiangVien,MaHocKy,NgayHoc,GioBatDau,GioKetThuc,PhongHoc")] LopHocPhan lopHocPhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lopHocPhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaGiangVien"] = new SelectList(_context.GiangViens, "MaNguoiDung", "MaNguoiDung", lopHocPhan.MaGiangVien);
            ViewData["MaHocKy"] = new SelectList(_context.HocKies, "MaHocKy", "MaHocKy", lopHocPhan.MaHocKy);
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "MaMonHoc", lopHocPhan.MaMonHoc);
            ViewData["PhongHoc"] = new SelectList(_context.PhongHocs, "MaPhong", "MaPhong", lopHocPhan.PhongHoc);
            return View(lopHocPhan);
        }

        // GET: LopHocPhan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }
            ViewData["MaGiangVien"] = new SelectList(_context.GiangViens, "MaNguoiDung", "MaNguoiDung", lopHocPhan.MaGiangVien);
            ViewData["MaHocKy"] = new SelectList(_context.HocKies, "MaHocKy", "MaHocKy", lopHocPhan.MaHocKy);
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "MaMonHoc", lopHocPhan.MaMonHoc);
            ViewData["PhongHoc"] = new SelectList(_context.PhongHocs, "MaPhong", "MaPhong", lopHocPhan.PhongHoc);
            return View(lopHocPhan);
        }

        // POST: LopHocPhan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaLopHocPhan,MaMonHoc,MaGiangVien,MaHocKy,NgayHoc,GioBatDau,GioKetThuc,PhongHoc")] LopHocPhan lopHocPhan)
        {
            if (id != lopHocPhan.MaLopHocPhan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lopHocPhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LopHocPhanExists(lopHocPhan.MaLopHocPhan))
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
            ViewData["MaGiangVien"] = new SelectList(_context.GiangViens, "MaNguoiDung", "MaNguoiDung", lopHocPhan.MaGiangVien);
            ViewData["MaHocKy"] = new SelectList(_context.HocKies, "MaHocKy", "MaHocKy", lopHocPhan.MaHocKy);
            ViewData["MaMonHoc"] = new SelectList(_context.MonHocs, "MaMonHoc", "MaMonHoc", lopHocPhan.MaMonHoc);
            ViewData["PhongHoc"] = new SelectList(_context.PhongHocs, "MaPhong", "MaPhong", lopHocPhan.PhongHoc);
            return View(lopHocPhan);
        }

        // GET: LopHocPhan/Delete/5
        public async Task<IActionResult> Delete(string id)
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

        // POST: LopHocPhan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhan != null)
            {
                _context.LopHocPhans.Remove(lopHocPhan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LopHocPhanExists(string id)
        {
            return _context.LopHocPhans.Any(e => e.MaLopHocPhan == id);
        }
    }
}
