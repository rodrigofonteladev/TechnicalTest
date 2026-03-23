using TechnicalTest.Data;
using TechnicalTest.Interfaces;
using TechnicalTest.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TechnicalTest.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string msg)
        {
            if (TempData["SessionExpired"] != null)
            {
                ViewBag.AlertMessage = TempData["SessionExpired"];
            }
            else if (msg == "expired")
            {
                ViewBag.AlertMessage = "Su sesión ha expirado debido a inactividad. Por favor, inicie sesión nuevamente.";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authService.LoginAsync(model);
            if (!result.Success)
            {
                if (result.IsLocked)
                {
                    return RedirectToAction("Lockout", "Auth", new { userName = result.ApplicationUser!.NormalizedUserName });
                }

                ModelState.AddModelError("", result.Message ?? "Error al iniciar sesion");
                return View(model);
            }

            var user = result.ApplicationUser;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout(bool isAutomatic = false)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (isAutomatic)
            {
                TempData["SessionExpired"] = "Su sesión ha expirado debido a inactividad. Por favor, inicie sesión nuevamente.";
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Lockout(string userName)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.NormalizedUserName == userName);

            double remainingSeconds = 0;

            if (user != null && user.LockoutEnd.HasValue)
            {
                var diff = user.LockoutEnd.Value - DateTime.UtcNow;
                remainingSeconds = diff.TotalSeconds > 0 ? diff.TotalSeconds : 0;
            }

            ViewBag.RemainingSeconds = Math.Floor(remainingSeconds);
            return View();
        }

        // Endpoint para flujo de activar usuario
        [HttpGet("Auth/Welcome")]
        public async Task<IActionResult> Welcome(string token)
        {
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.ActivationToken == token);

            if (user == null) return NotFound();

            user.IsActive = true;
            user.ActivationToken = null;
            await _context.SaveChangesAsync();

            ViewBag.FirstName = user.FirstName;
            return View("Welcome", user.FirstName);
        }
    }
}
