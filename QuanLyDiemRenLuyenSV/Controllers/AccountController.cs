using Microsoft.AspNetCore.Mvc;
using QuanLyDiemRenLuyenSV.Data;
using QuanLyDiemRenLuyenSV.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net; // Cần cài đặt gói NuGet BCrypt.Net-Next

namespace QuanLyDiemRenLuyenSV.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken] // Bảo vệ chống lại tấn công CSRF
        public async Task<IActionResult> Login(string username, string password, int vaiTroID)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không được để trống.");
                return View();
            }

            var nguoiDung = await _context.NguoiDungs
                                        .Include(nd => nd.VaiTro) // Include VaiTro để lấy tên vai trò
                                        .FirstOrDefaultAsync(nd => nd.Username == username && nd.VaiTroID == vaiTroID);

            if (nguoiDung == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập, mật khẩu hoặc vai trò không đúng.");
                return View();
            }

            // Kiểm tra mật khẩu (sử dụng BCrypt để so sánh hash)
            // Ban đầu, mật khẩu mặc định của SV là 1111, NV là 0000
            // Bạn cần hash các mật khẩu này và lưu vào CSDL trước khi triển khai
            // Ví dụ: string hashedPassword = BCrypt.Net.BCrypt.HashPassword("1111");
            // Sau đó so sánh: BCrypt.Net.BCrypt.Verify(password, nguoiDung.PasswordHash)

            // Tạm thời kiểm tra mật khẩu mặc định (chỉ dùng cho mục đích demo/dev ban đầu)
            bool isPasswordValid = false;
            if (nguoiDung.VaiTro?.TenVaiTro == "SinhVien" && password == "1111")
            {
                isPasswordValid = true;
            }
            else if ((nguoiDung.VaiTro?.TenVaiTro == "GiaoVienChuNhiem" || nguoiDung.VaiTro?.TenVaiTro == "ThanhVienHoiDong" || nguoiDung.VaiTro?.TenVaiTro == "Admin" || nguoiDung.VaiTro?.TenVaiTro == "PhongDaoTao") && password == "0000")
            {
                isPasswordValid = true;
            }
            // Nếu bạn đã hash mật khẩu trong CSDL, hãy sử dụng:
            // else if (BCrypt.Net.BCrypt.Verify(password, nguoiDung.PasswordHash))
            // {
            //     isPasswordValid = true;
            // }


            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Tên đăng nhập, mật khẩu hoặc vai trò không đúng.");
                return View();
            }

            // Cập nhật LastLogin
            nguoiDung.LastLogin = DateTime.Now;
            _context.Update(nguoiDung);
            await _context.SaveChangesAsync();

            // Tạo ClaimsIdentity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, nguoiDung.NguoiDungID.ToString()),
                new Claim(ClaimTypes.Name, nguoiDung.Username),
                new Claim(ClaimTypes.Role, nguoiDung.VaiTro?.TenVaiTro ?? "Unknown"), // Thêm vai trò
                new Claim("VaiTroID", nguoiDung.VaiTroID.ToString()!) // Lưu VaiTroID
            };

            // Thêm thông tin cá nhân vào claims tùy theo vai trò
            if (nguoiDung.SinhVienID.HasValue)
            {
                var sinhVien = await _context.SinhViens.FirstOrDefaultAsync(sv => sv.SinhVienID == nguoiDung.SinhVienID.Value);
                if (sinhVien != null)
                {
                    claims.Add(new Claim("HoTen", sinhVien.HoTen));
                    claims.Add(new Claim("MaSV", sinhVien.MaSV));
                    claims.Add(new Claim("SinhVienID", sinhVien.SinhVienID.ToString()));
                }
            }
            else if (nguoiDung.NhanVienID.HasValue)
            {
                var nhanVien = await _context.NhanViens.FirstOrDefaultAsync(nv => nv.NhanVienID == nguoiDung.NhanVienID.Value);
                if (nhanVien != null)
                {
                    claims.Add(new Claim("HoTen", nhanVien.HoTen));
                    claims.Add(new Claim("MaNV", nhanVien.MaNV));
                    claims.Add(new Claim("NhanVienID", nhanVien.NhanVienID.ToString()));
                }
            }


            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Ghi nhớ đăng nhập
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Thời gian hết hạn
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Chuyển hướng sau khi đăng nhập thành công dựa trên vai trò
            switch (nguoiDung.VaiTro?.TenVaiTro)
            {
                case "Admin":
                    return RedirectToAction("Index", "Admin"); // Sẽ tạo Controller Admin sau
                case "SinhVien":
                    return RedirectToAction("Dashboard", "SinhVien"); // Sẽ tạo Controller Student sau
                case "GiaoVienChuNhiem":
                    return RedirectToAction("Dashboard", "GVCN"); // Sẽ tạo Controller GVCN sau
                case "ThanhVienHoiDong":
                    return RedirectToAction("Dashboard", "HoiDong"); // Sẽ tạo Controller HoiDong sau
                default:
                    return RedirectToAction("Index", "Home"); // Trang mặc định
            }
        }

        // GET: /Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: /Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // GET: /Account/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
            {
                ModelState.AddModelError("", "Tất cả các trường không được để trống.");
                return View();
            }

            if (newPassword != confirmNewPassword)
            {
                ModelState.AddModelError("", "Mật khẩu mới và xác nhận mật khẩu mới không khớp.");
                return View();
            }

            var username = User.Identity?.Name;
            if (username == null)
            {
                ModelState.AddModelError("", "Không tìm thấy thông tin người dùng.");
                return View();
            }

            var nguoiDung = await _context.NguoiDungs.FirstOrDefaultAsync(nd => nd.Username == username);

            if (nguoiDung == null)
            {
                ModelState.AddModelError("", "Người dùng không tồn tại.");
                return View();
            }

            // So sánh mật khẩu cũ (cần sử dụng BCrypt.Net.BCrypt.Verify nếu đã hash)
            // Tạm thời kiểm tra mật khẩu mặc định hoặc so sánh trực tiếp nếu bạn chưa hash
            bool isOldPasswordValid = false;
            if (nguoiDung.VaiTro?.TenVaiTro == "SinhVien" && oldPassword == "1111")
            {
                isOldPasswordValid = true;
            }
            else if ((nguoiDung.VaiTro?.TenVaiTro == "GiaoVienChuNhiem" || nguoiDung.VaiTro?.TenVaiTro == "ThanhVienHoiDong" || nguoiDung.VaiTro?.TenVaiTro == "Admin") && oldPassword == "0000")
            {
                isOldPasswordValid = true;
            }
            // Nếu bạn đã hash mật khẩu trong CSDL, hãy sử dụng:
            // else if (BCrypt.Net.BCrypt.Verify(oldPassword, nguoiDung.PasswordHash))
            // {
            //     isOldPasswordValid = true;
            // }


            if (!isOldPasswordValid)
            {
                ModelState.AddModelError("", "Mật khẩu cũ không đúng.");
                return View();
            }

            // Hash mật khẩu mới và cập nhật
            nguoiDung.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword); // Hash mật khẩu mới
            _context.Update(nguoiDung);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("ChangePassword"); // Chuyển hướng về trang đổi mật khẩu với thông báo
        }
    }
}