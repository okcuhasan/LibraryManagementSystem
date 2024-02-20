using KutuphaneYonetimSistemi.Data.DTO;
using KutuphaneYonetimSistemi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class GirisYapController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public GirisYapController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GirisYap(GirisDTO dto)
        {
            if (ModelState.IsValid)
            {
                var giris = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Sifre, false, false);

                if (giris.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var kullanici = await _userManager.FindByNameAsync(dto.UserName);

                    if (kullanici == null)
                    {
                        ModelState.AddModelError("UserName", "Kullanıcı Adı hatalı!");
                    }
                    else if (kullanici.Sifre != dto.Sifre)
                    {
                        ModelState.AddModelError("Sifre", "Şifre hatalı!");
                    }
                }
                return View(dto);
            }
            return View(dto);
        }
    }
}
