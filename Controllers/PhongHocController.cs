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
    public class PhongHocController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public PhongHocController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: PhongHoc
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhongHocs.ToListAsync());
        }

        // GET: PhongHoc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongHoc = await _context.PhongHocs
                .FirstOrDefaultAsync(m => m.MaPhong == id);
            if (phongHoc == null)
            {
                return NotFound();
            }

            return View(phongHoc);
        }

        // GET: PhongHoc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhongHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaPhong,Tang,Khu,LoaiPhong")] PhongHoc phongHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phongHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phongHoc);
        }

        // GET: PhongHoc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongHoc = await _context.PhongHocs.FindAsync(id);
            if (phongHoc == null)
            {
                return NotFound();
            }
            return View(phongHoc);
        }

        // POST: PhongHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaPhong,Tang,Khu,LoaiPhong")] PhongHoc phongHoc)
        {
            if (id != phongHoc.MaPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phongHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhongHocExists(phongHoc.MaPhong))
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
            return View(phongHoc);
        }

        // GET: PhongHoc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongHoc = await _context.PhongHocs
                .FirstOrDefaultAsync(m => m.MaPhong == id);
            if (phongHoc == null)
            {
                return NotFound();
            }

            return View(phongHoc);
        }

        // POST: PhongHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var phongHoc = await _context.PhongHocs.FindAsync(id);
            if (phongHoc != null)
            {
                _context.PhongHocs.Remove(phongHoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongHocExists(string id)
        {
            return _context.PhongHocs.Any(e => e.MaPhong == id);
        }
    }
}
