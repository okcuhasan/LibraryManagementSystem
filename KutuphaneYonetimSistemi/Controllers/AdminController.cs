using KutuphaneYonetimSistemi.Data;
using KutuphaneYonetimSistemi.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult AdminKayit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminKayit(AdminDTO dto)
        {
            if (ModelState.IsValid)
            {
                var admin = new ApplicationUser { UserName = dto.UserName, Sifre = dto.Sifre, SifreTekrar = dto.SifreTekrar };

                var result = await _userManager.CreateAsync(admin, dto.Sifre);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");

                    return RedirectToAction("GirisYap", "GirisYap");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "PasswordRequiresDigit")
                        {
                            ModelState.AddModelError("Sifre", "Şifreniz en az bir sayı içermelidir!");
                        }
                        else if (error.Code == "PasswordRequiresLower")
                        {
                            ModelState.AddModelError("Sifre", "Şifreniz en az bir küçük harf içermelidir!");
                        }
                        else if (error.Code == "PasswordRequiresUpper")
                        {
                            ModelState.AddModelError("Sifre", "Şifreniz en az bir büyük harf içermelidir!");
                        }
                        else if (error.Code == "PasswordRequiresNonAlphanumeric")
                        {
                            ModelState.AddModelError("Sifre", "Şifreniz en az bir özel karakter içermelidir!");
                        }
                        else if (error.Code == "PasswordTooShort")
                        {
                            ModelState.AddModelError("Sifre", "Şifreniz en az 8 karakter olmalıdır!");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    return View(dto);
                }
            }
            return View(dto);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OduncAlinanKitapGoruntule()
        {
            var kullaniciOdunc = await _userManager.Users
                .Include(x => x.Odunc)
                .ThenInclude(x => x.Kitap)
                .ToListAsync();

            return View(kullaniciOdunc);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KullaniciIadeEdilenKitaplar()
        {
            var kullaniciOdunc = await _userManager.Users
                .Include(x => x.Iade)
                .ThenInclude(x => x.Kitap)
                .ToListAsync();

            return View(kullaniciOdunc);
        }



    }
}
