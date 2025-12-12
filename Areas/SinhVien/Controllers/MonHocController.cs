using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    public class MonHocController : Controller
    {
        private readonly IMonHocService _service;

        public MonHocController(IMonHocService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            // Sinh viên chỉ xem, giao diện sẽ khác Admin (ít nút bấm hơn)
            // Tạm thời lấy hết, sau này lọc theo Mã Sinh Viên
            var data = _service.GetAll();
            return View(data);
        }
    }
}
