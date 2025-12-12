using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MonHocController : Controller
    {
        private readonly IMonHocService _service;

        public MonHocController(IMonHocService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            // Admin thấy toàn bộ danh sách, có thể sau này thêm nút Sửa/Xóa
            var data = _service.GetAll();
            return View(data);
        }
    }
}
