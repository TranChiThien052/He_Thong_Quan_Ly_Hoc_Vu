using Microsoft.AspNetCore.Mvc;

namespace QuanLyHocVu.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
