using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebMusica.Models;

namespace WebMusica.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly MusicaContext _context;

        public AccountController(MusicaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Intentos de inicio de sesión no válidos.");
                return View(model);
            }

            var usuario = _context.Usuarios
                .Where(x => x.Estado == 1 && x.Usuario1 == model.usuario &&
                    x.Clave == Util.Encrypt(model.clave)).FirstOrDefault();
            if (usuario != null)
            {
                TempData["isLogged"] = true;
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Usuario1),
                new Claim(ClaimTypes.Email, usuario.Correo)
                //new Claim(ClaimTypes.Role, usuario.EsAdmin)
                
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1),
                    IsPersistent = model.recordarme
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (returnUrl == null) returnUrl = ViewData["ReturnUrl"]?.ToString();
                if (returnUrl != null) return Redirect(returnUrl);
                else return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                ModelState.AddModelError("", "Intentos de inicio de sesión no válidos.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            TempData["isLogged"] = false;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
