using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Services;

namespace QuanLyHocVu.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    public class DangKyHocPhanController : Controller
    {
        private readonly IDangKyHocPhanService _service;

        public DangKyHocPhanController(IDangKyHocPhanService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            // Trong thực tế sẽ lấy ID sinh viên từ User.Claims
            // var maSinhVien = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // return View(_service.GetBySinhVien(maSinhVien));
            
            // Tạm thời trả về view trống hoặc list rỗng để demo
            return View();
        }
    }
}
