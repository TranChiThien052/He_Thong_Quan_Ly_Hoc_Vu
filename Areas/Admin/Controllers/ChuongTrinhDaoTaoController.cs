using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;
using System.Text.Json;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChuongTrinhDaoTaoController : Controller
    {
        private readonly IChuongTrinhDaoTaoService _chuongTrinhDaoTaoservice;
        private readonly INganhService _nganhService;
        private readonly IMonHocService _monHocService;
        private readonly IChiTietChuongTrinhService _chiTietService;

        public ChuongTrinhDaoTaoController(IChuongTrinhDaoTaoService chuongTrinhDaoTaoservice, INganhService nganhService, IMonHocService monHocService, IChiTietChuongTrinhService chiTietService)
        {
            _chuongTrinhDaoTaoservice = chuongTrinhDaoTaoservice;
            _nganhService = nganhService;
            _monHocService = monHocService;
            _chiTietService = chiTietService;
        }

        public IActionResult Index()
        {
            
            var nganhs = _nganhService.GetAll();
            ViewBag.Nganhs = new SelectList(nganhs, "MaNganh", "TenNganh");
            var ctdts = _chuongTrinhDaoTaoservice.GetAll();
            return View(ctdts);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var ctdt = _chuongTrinhDaoTaoservice.GetById(id);
            if (ctdt == null) return NotFound();

            var allMonHoc = _monHocService.GetAll().Select(m => new { m.MaMonHoc, m.TenMonHoc }).ToList();
            var chiTietList = _chiTietService.GetByMaCTDT(id)
                .Select(ct => new { 
                    MaMonHoc = ct.MaMonHoc, 
                    TenMonHoc = ct.MaMonHocNavigation.TenMonHoc, 
                    HocKy = ct.HocKy 
                }).ToList();

            ViewBag.AllMonHocJson = JsonSerializer.Serialize(allMonHoc);
            ViewBag.ChiTietJson = JsonSerializer.Serialize(chiTietList);
            
            // For SelectLists
            ViewBag.Nganhs = new SelectList(_nganhService.GetAll(), "MaNganh", "TenNganh", ctdt.MaNganh);
            
            return View(ctdt);
        }

        [HttpPost]
        public IActionResult SaveChiTiet(string maCTDT, [FromBody] List<ChiTietChuongTrinhDaoTao> chiTietList)
        {
            if (string.IsNullOrEmpty(maCTDT))
                return BadRequest("Mã CTĐT không hợp lệ");

            try 
            {
                _chiTietService.UpdateChiTiet(maCTDT, chiTietList);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(string maCTDTMoi, string tenCTDTMoi, string nganhMoi)
        {
            if (string.IsNullOrEmpty(maCTDTMoi) || string.IsNullOrEmpty(tenCTDTMoi) || string.IsNullOrEmpty(nganhMoi))
            {
                 // Handle error - for now redirecting back or returning view with error
                 return RedirectToAction("Index");
            }

            var ctdt = new ChuongTrinhDaoTao
            {
                MaCtdt = maCTDTMoi,
                TenCtdt = tenCTDTMoi,
                MaNganh = nganhMoi,
               // TongTinChi = 0 // Default value
            };

            try
            {
                _chuongTrinhDaoTaoservice.Add(ctdt);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Log error
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _chuongTrinhDaoTaoservice.Delete(id);
            }
            return RedirectToAction("Index");
        }
    }
}