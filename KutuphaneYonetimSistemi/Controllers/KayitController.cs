using KutuphaneYonetimSistemi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class KayitController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public KayitController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kayit(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser kullanici = new ApplicationUser()
                {
                    Ad = model.Ad,
                    SoyAd = model.SoyAd,
                    Sifre = model.Sifre,
                    SifreTekrar = model.SifreTekrar,
                    UserName = model.UserName,
                    Email = model.EMailAdresi,
                    EMailAdresi = model.EMailAdresi
                };

                var result = await _userManager.CreateAsync(kullanici, model.Sifre);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(kullanici, "Kullanici");

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
                        else if (error.Code == "DuplicateUserName")
                        {
                            ModelState.AddModelError("EMailAdresi", "Bu e posta adresi zaten kullanılmaktadır!");
                        }
                        else if (error.Code == "InvalidUserName")
                        {
                            ModelState.AddModelError("EMailAdresi", "E-Posta alanı boş geçilemez!");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    return View(model);
                }
            }

            return View(model);
        }

    }
}
