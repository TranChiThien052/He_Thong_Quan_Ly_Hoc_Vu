using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuanLyHocVu.Models.QuanLyHocVuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Đăng ký Service vào Container
builder.Services.AddScoped<QuanLyHocVu.Services.IMonHocService, QuanLyHocVu.Services.MonHocService>();
builder.Services.AddScoped<QuanLyHocVu.Services.ISinhVienService, QuanLyHocVu.Services.SinhVienService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IGiangVienService, QuanLyHocVu.Services.GiangVienService>();
builder.Services.AddScoped<QuanLyHocVu.Services.ICanBoService, QuanLyHocVu.Services.CanBoService>();
builder.Services.AddScoped<QuanLyHocVu.Services.ILopHocPhanService, QuanLyHocVu.Services.LopHocPhanService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IDangKyHocPhanService, QuanLyHocVu.Services.DangKyHocPhanService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IKhoaService, QuanLyHocVu.Services.KhoaService>();
builder.Services.AddScoped<QuanLyHocVu.Services.INganhService, QuanLyHocVu.Services.NganhService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IHocKyService, QuanLyHocVu.Services.HocKyService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IPhongHocService, QuanLyHocVu.Services.PhongHocService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IChuongTrinhDaoTaoService, QuanLyHocVu.Services.ChuongTrinhDaoTaoService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IHocPhiService, QuanLyHocVu.Services.HocPhiService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IKhoaNganhService, QuanLyHocVu.Services.KhoaNganhService>();
builder.Services.AddScoped<QuanLyHocVu.Services.IChiTietChuongTrinhService, QuanLyHocVu.Services.ChiTietChuongTrinhService>();


// Thêm Authentication Service
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // BẮT BUỘC: Phải đứng trước Authorization
app.UseAuthorization();

// Định tuyến cho Areas (QUAN TRỌNG: Phải đặt trước route mặc định)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
