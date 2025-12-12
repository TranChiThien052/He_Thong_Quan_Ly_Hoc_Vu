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
    public class ChuongTrinhDaoTaoController : Controller
    {
        private readonly QuanLyHocVuContext _context;

        public ChuongTrinhDaoTaoController(QuanLyHocVuContext context)
        {
            _context = context;
        }

        // GET: ChuongTrinhDaoTao
        public async Task<IActionResult> Index()
        {
            var quanLyHocVuContext = _context.ChuongTrinhDaoTaos.Include(c => c.MaNganhNavigation);
            return View(await quanLyHocVuContext.ToListAsync());
        }

        // GET: ChuongTrinhDaoTao/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongTrinhDaoTao = await _context.ChuongTrinhDaoTaos
                .Include(c => c.MaNganhNavigation)
                .FirstOrDefaultAsync(m => m.MaCtdt == id);
            if (chuongTrinhDaoTao == null)
            {
                return NotFound();
            }

            return View(chuongTrinhDaoTao);
        }

        // GET: ChuongTrinhDaoTao/Create
        public IActionResult Create()
        {
            ViewData["MaNganh"] = new SelectList(_context.Nganhs, "MaNganh", "MaNganh");
            return View();
        }

        // POST: ChuongTrinhDaoTao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCtdt,TenCtdt,MaNganh")] ChuongTrinhDaoTao chuongTrinhDaoTao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuongTrinhDaoTao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNganh"] = new SelectList(_context.Nganhs, "MaNganh", "MaNganh", chuongTrinhDaoTao.MaNganh);
            return View(chuongTrinhDaoTao);
        }

        // GET: ChuongTrinhDaoTao/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongTrinhDaoTao = await _context.ChuongTrinhDaoTaos.FindAsync(id);
            if (chuongTrinhDaoTao == null)
            {
                return NotFound();
            }
            ViewData["MaNganh"] = new SelectList(_context.Nganhs, "MaNganh", "MaNganh", chuongTrinhDaoTao.MaNganh);
            return View(chuongTrinhDaoTao);
        }

        // POST: ChuongTrinhDaoTao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaCtdt,TenCtdt,MaNganh")] ChuongTrinhDaoTao chuongTrinhDaoTao)
        {
            if (id != chuongTrinhDaoTao.MaCtdt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuongTrinhDaoTao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuongTrinhDaoTaoExists(chuongTrinhDaoTao.MaCtdt))
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
            ViewData["MaNganh"] = new SelectList(_context.Nganhs, "MaNganh", "MaNganh", chuongTrinhDaoTao.MaNganh);
            return View(chuongTrinhDaoTao);
        }

        // GET: ChuongTrinhDaoTao/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongTrinhDaoTao = await _context.ChuongTrinhDaoTaos
                .Include(c => c.MaNganhNavigation)
                .FirstOrDefaultAsync(m => m.MaCtdt == id);
            if (chuongTrinhDaoTao == null)
            {
                return NotFound();
            }

            return View(chuongTrinhDaoTao);
        }

        // POST: ChuongTrinhDaoTao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chuongTrinhDaoTao = await _context.ChuongTrinhDaoTaos.FindAsync(id);
            if (chuongTrinhDaoTao != null)
            {
                _context.ChuongTrinhDaoTaos.Remove(chuongTrinhDaoTao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuongTrinhDaoTaoExists(string id)
        {
            return _context.ChuongTrinhDaoTaos.Any(e => e.MaCtdt == id);
        }
    }
}
