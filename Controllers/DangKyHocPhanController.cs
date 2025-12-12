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
    public class DangKyHocPhanController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public DangKyHocPhanController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: DangKyHocPhan
        public async Task<IActionResult> Index()
        {
            var quanLyHocVuContext = _context.DangKyHocPhans.Include(d => d.MaLopHocPhanNavigation).Include(d => d.MaSinhVienNavigation);
            return View(await quanLyHocVuContext.ToListAsync());
        }

        // GET: DangKyHocPhan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dangKyHocPhan = await _context.DangKyHocPhans
                .Include(d => d.MaLopHocPhanNavigation)
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (dangKyHocPhan == null)
            {
                return NotFound();
            }

            return View(dangKyHocPhan);
        }

        // GET: DangKyHocPhan/Create
        public IActionResult Create()
        {
            ViewData["MaLopHocPhan"] = new SelectList(_context.LopHocPhans, "MaLopHocPhan", "MaLopHocPhan");
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung");
            return View();
        }

        // POST: DangKyHocPhan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSinhVien,MaLopHocPhan,TrangThai")] DangKyHocPhan dangKyHocPhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dangKyHocPhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLopHocPhan"] = new SelectList(_context.LopHocPhans, "MaLopHocPhan", "MaLopHocPhan", dangKyHocPhan.MaLopHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", dangKyHocPhan.MaSinhVien);
            return View(dangKyHocPhan);
        }

        // GET: DangKyHocPhan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dangKyHocPhan = await _context.DangKyHocPhans.FindAsync(id);
            if (dangKyHocPhan == null)
            {
                return NotFound();
            }
            ViewData["MaLopHocPhan"] = new SelectList(_context.LopHocPhans, "MaLopHocPhan", "MaLopHocPhan", dangKyHocPhan.MaLopHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", dangKyHocPhan.MaSinhVien);
            return View(dangKyHocPhan);
        }

        // POST: DangKyHocPhan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSinhVien,MaLopHocPhan,TrangThai")] DangKyHocPhan dangKyHocPhan)
        {
            if (id != dangKyHocPhan.MaSinhVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dangKyHocPhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DangKyHocPhanExists(dangKyHocPhan.MaSinhVien))
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
            ViewData["MaLopHocPhan"] = new SelectList(_context.LopHocPhans, "MaLopHocPhan", "MaLopHocPhan", dangKyHocPhan.MaLopHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhViens, "MaNguoiDung", "MaNguoiDung", dangKyHocPhan.MaSinhVien);
            return View(dangKyHocPhan);
        }

        // GET: DangKyHocPhan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dangKyHocPhan = await _context.DangKyHocPhans
                .Include(d => d.MaLopHocPhanNavigation)
                .Include(d => d.MaSinhVienNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (dangKyHocPhan == null)
            {
                return NotFound();
            }

            return View(dangKyHocPhan);
        }

        // POST: DangKyHocPhan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dangKyHocPhan = await _context.DangKyHocPhans.FindAsync(id);
            if (dangKyHocPhan != null)
            {
                _context.DangKyHocPhans.Remove(dangKyHocPhan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DangKyHocPhanExists(string id)
        {
            return _context.DangKyHocPhans.Any(e => e.MaSinhVien == id);
        }
    }
}
