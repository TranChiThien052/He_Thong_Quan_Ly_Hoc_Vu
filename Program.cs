using Microsoft.EntityFrameworkCore;
using QuanLyHocVu.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuanLyHocVu.Models.QuanLyHocVuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMonHocService, MonHocService>();
builder.Services.AddScoped<ISinhVienService, SinhVienService>();
builder.Services.AddScoped<IGiangVienService, GiangVienService>();
builder.Services.AddScoped<ILopHocPhanService, LopHocPhanService>();
builder.Services.AddScoped<IDangKyHocPhanService, DangKyHocPhanService>();
builder.Services.AddScoped<IKhoaService, KhoaService>();
builder.Services.AddScoped<INganhService, NganhService>();
builder.Services.AddScoped<IHocKyService, HocKyService>();
builder.Services.AddScoped<IPhongHocService, PhongHocService>();
builder.Services.AddScoped<IChuongTrinhDaoTaoService, ChuongTrinhDaoTaoService>();
builder.Services.AddScoped<IHocPhiService, HocPhiService>();
builder.Services.AddScoped<IKhoaNganhService, KhoaNganhService>();
builder.Services.AddScoped<IChiTietChuongTrinhService, ChiTietChuongTrinhService>();
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();
builder.Services.AddScoped<IDiemRenLuyenService, DiemRenLuyenService>();
builder.Services.AddScoped<IDiemCongTacXaHoiService, DiemCongTacXaHoiService>();
builder.Services.AddScoped<IDiemHocPhanService, DiemHocPhanService>();

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

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
