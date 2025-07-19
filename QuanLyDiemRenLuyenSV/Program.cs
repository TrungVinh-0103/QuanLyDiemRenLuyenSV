using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuanLyDiemRenLuyenSV.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); // 👈 Thêm dòng này
//=======================================================|
// Đăng ký ApplicationDbContext với chuỗi kết nối
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm Razor Runtime Compilation
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Cấu hình Authentication Service (Cookie Authentication)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Đường dẫn đến trang đăng nhập
        options.LogoutPath = "/Account/Logout"; // Đường dẫn đến trang đăng xuất
        options.AccessDeniedPath = "/Account/AccessDenied"; // Đường dẫn khi truy cập bị từ chối
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian hết hạn của cookie
        options.SlidingExpiration = true; // Gia hạn cookie nếu người dùng hoạt động
    });

// Thêm Authorization Service (quan trọng để sử dụng [Authorize])
builder.Services.AddAuthorization();
//=======================================================|
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
