using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuanLyHocVu.Models;

namespace QuanLyHocVu.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

