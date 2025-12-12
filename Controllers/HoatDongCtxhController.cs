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
    public class HoatDongCtxhController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public HoatDongCtxhController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: HoatDongCtxh
        public async Task<IActionResult> Index()
        {
            return View(await _context.HoatDongCtxhs.ToListAsync());
        }

        // GET: HoatDongCtxh/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDongCtxh = await _context.HoatDongCtxhs
                .FirstOrDefaultAsync(m => m.MaHoatDong == id);
            if (hoatDongCtxh == null)
            {
                return NotFound();
            }

            return View(hoatDongCtxh);
        }

        // GET: HoatDongCtxh/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HoatDongCtxh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHoatDong,TenHoatDong,Diem,NgayToChuc,GhiChu")] HoatDongCtxh hoatDongCtxh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoatDongCtxh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoatDongCtxh);
        }

        // GET: HoatDongCtxh/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDongCtxh = await _context.HoatDongCtxhs.FindAsync(id);
            if (hoatDongCtxh == null)
            {
                return NotFound();
            }
            return View(hoatDongCtxh);
        }

        // POST: HoatDongCtxh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHoatDong,TenHoatDong,Diem,NgayToChuc,GhiChu")] HoatDongCtxh hoatDongCtxh)
        {
            if (id != hoatDongCtxh.MaHoatDong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoatDongCtxh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoatDongCtxhExists(hoatDongCtxh.MaHoatDong))
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
            return View(hoatDongCtxh);
        }

        // GET: HoatDongCtxh/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDongCtxh = await _context.HoatDongCtxhs
                .FirstOrDefaultAsync(m => m.MaHoatDong == id);
            if (hoatDongCtxh == null)
            {
                return NotFound();
            }

            return View(hoatDongCtxh);
        }

        // POST: HoatDongCtxh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hoatDongCtxh = await _context.HoatDongCtxhs.FindAsync(id);
            if (hoatDongCtxh != null)
            {
                _context.HoatDongCtxhs.Remove(hoatDongCtxh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoatDongCtxhExists(string id)
        {
            return _context.HoatDongCtxhs.Any(e => e.MaHoatDong == id);
        }
    }
}
