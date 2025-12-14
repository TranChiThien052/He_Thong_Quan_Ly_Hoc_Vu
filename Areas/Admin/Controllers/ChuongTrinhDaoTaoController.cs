using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Models;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChuongTrinhDaoTaoController : Controller
    {
        private readonly IChuongTrinhDaoTaoService _chuongTrinhDaoTaoservice;
        private readonly INganhService _nganhService;
        private readonly IMonHocService _monHocService;

        public ChuongTrinhDaoTaoController(IChuongTrinhDaoTaoService chuongTrinhDaoTaoservice, INganhService nganhService, IMonHocService monHocService)
        {
            _chuongTrinhDaoTaoservice = chuongTrinhDaoTaoservice;
            _nganhService = nganhService;
            _monHocService = monHocService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}