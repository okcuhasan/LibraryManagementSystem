using KutuphaneYonetimSistemi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class CikisController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CikisController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> CikisYap()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("GirisYap", "GirisYap");
        }
    }
}
