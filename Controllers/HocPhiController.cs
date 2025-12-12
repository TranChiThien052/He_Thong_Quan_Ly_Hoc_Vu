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
    public class HocPhiController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public HocPhiController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: HocPhi
        public async Task<IActionResult> Index()
        {
            return View(await _context.HocPhis.ToListAsync());
        }

        // GET: HocPhi/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhi = await _context.HocPhis
                .FirstOrDefaultAsync(m => m.MaHocPhi == id);
            if (hocPhi == null)
            {
                return NotFound();
            }

            return View(hocPhi);
        }

        // GET: HocPhi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HocPhi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHocPhi,GiaTheoTin")] HocPhi hocPhi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocPhi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hocPhi);
        }

        // GET: HocPhi/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhi = await _context.HocPhis.FindAsync(id);
            if (hocPhi == null)
            {
                return NotFound();
            }
            return View(hocPhi);
        }

        // POST: HocPhi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHocPhi,GiaTheoTin")] HocPhi hocPhi)
        {
            if (id != hocPhi.MaHocPhi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocPhi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocPhiExists(hocPhi.MaHocPhi))
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
            return View(hocPhi);
        }

        // GET: HocPhi/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhi = await _context.HocPhis
                .FirstOrDefaultAsync(m => m.MaHocPhi == id);
            if (hocPhi == null)
            {
                return NotFound();
            }

            return View(hocPhi);
        }

        // POST: HocPhi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hocPhi = await _context.HocPhis.FindAsync(id);
            if (hocPhi != null)
            {
                _context.HocPhis.Remove(hocPhi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocPhiExists(string id)
        {
            return _context.HocPhis.Any(e => e.MaHocPhi == id);
        }
    }
}
