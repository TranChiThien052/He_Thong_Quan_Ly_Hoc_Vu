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
    public class DiemRenLuyenController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public DiemRenLuyenController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: DiemRenLuyen
        public async Task<IActionResult> Index()
        {
            var quanLyHocVuContext = _context.DiemRenLuyens.Include(d => d.MaHocKyNavigation).Include(d => d.MaSinhVienNavigation);
            return View(await quanLyHocVuContext.ToListAsync());
        }

        // GET: DiemRenLuyen/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diemRenLuyen = await _context.DiemRenLuyens
                .Include(d => d.MaHocKyNavigation)
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (diemRenLuyen == null)
            {
                return NotFound();
            }

            return View(diemRenLuyen);
        }

        // GET: DiemRenLuyen/Create
        public IActionResult Create()
        {
            ViewData["MaHocKy"] = new SelectList(_context.HocKies, "MaHocKy", "MaHocKy");
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung");
            return View();
        }

        // POST: DiemRenLuyen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSinhVien,MaHocKy,Diem,XepLoai,GhiChu")] DiemRenLuyen diemRenLuyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diemRenLuyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHocKy"] = new SelectList(_context.HocKies, "MaHocKy", "MaHocKy", diemRenLuyen.MaHocKy);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", diemRenLuyen.MaSinhVien);
            return View(diemRenLuyen);
        }

        // GET: DiemRenLuyen/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diemRenLuyen = await _context.DiemRenLuyens.FindAsync(id);
            if (diemRenLuyen == null)
            {
                return NotFound();
            }
            ViewData["MaHocKy"] = new SelectList(_context.HocKies, "MaHocKy", "MaHocKy", diemRenLuyen.MaHocKy);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", diemRenLuyen.MaSinhVien);
            return View(diemRenLuyen);
        }

        // POST: DiemRenLuyen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSinhVien,MaHocKy,Diem,XepLoai,GhiChu")] DiemRenLuyen diemRenLuyen)
        {
            if (id != diemRenLuyen.MaSinhVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diemRenLuyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiemRenLuyenExists(diemRenLuyen.MaSinhVien))
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
            ViewData["MaHocKy"] = new SelectList(_context.HocKies, "MaHocKy", "MaHocKy", diemRenLuyen.MaHocKy);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", diemRenLuyen.MaSinhVien);
            return View(diemRenLuyen);
        }

        // GET: DiemRenLuyen/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diemRenLuyen = await _context.DiemRenLuyens
                .Include(d => d.MaHocKyNavigation)
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (diemRenLuyen == null)
            {
                return NotFound();
            }

            return View(diemRenLuyen);
        }

        // POST: DiemRenLuyen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var diemRenLuyen = await _context.DiemRenLuyens.FindAsync(id);
            if (diemRenLuyen != null)
            {
                _context.DiemRenLuyens.Remove(diemRenLuyen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiemRenLuyenExists(string id)
        {
            return _context.DiemRenLuyens.Any(e => e.MaSinhVien == id);
        }
    }
}
