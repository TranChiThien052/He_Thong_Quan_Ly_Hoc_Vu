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
    public class CanBoController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public CanBoController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: CanBo
        public async Task<IActionResult> Index()
        {
            var quanLyHocVuContext = _context.CanBos.Include(c => c.MaNguoiDungNavigation);
            return View(await quanLyHocVuContext.ToListAsync());
        }

        // GET: CanBo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canBo = await _context.CanBos
                .Include(c => c.MaNguoiDungNavigation)
                .FirstOrDefaultAsync(m => m.MaNguoiDung == id);
            if (canBo == null)
            {
                return NotFound();
            }

            return View(canBo);
        }

        // GET: CanBo/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "MaNguoiDung");
            return View();
        }

        // POST: CanBo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNguoiDung,TinhTrangCongTac")] CanBo canBo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(canBo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "MaNguoiDung", canBo.MaNguoiDung);
            return View(canBo);
        }

        // GET: CanBo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canBo = await _context.CanBos.FindAsync(id);
            if (canBo == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "MaNguoiDung", canBo.MaNguoiDung);
            return View(canBo);
        }

        // POST: CanBo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNguoiDung,TinhTrangCongTac")] CanBo canBo)
        {
            if (id != canBo.MaNguoiDung)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canBo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanBoExists(canBo.MaNguoiDung))
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
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "MaNguoiDung", canBo.MaNguoiDung);
            return View(canBo);
        }

        // GET: CanBo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canBo = await _context.CanBos
                .Include(c => c.MaNguoiDungNavigation)
                .FirstOrDefaultAsync(m => m.MaNguoiDung == id);
            if (canBo == null)
            {
                return NotFound();
            }

            return View(canBo);
        }

        // POST: CanBo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var canBo = await _context.CanBos.FindAsync(id);
            if (canBo != null)
            {
                _context.CanBos.Remove(canBo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CanBoExists(string id)
        {
            return _context.CanBos.Any(e => e.MaNguoiDung == id);
        }
    }
}
