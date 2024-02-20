using KutuphaneYonetimSistemi.Data;
using KutuphaneYonetimSistemi.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    [Authorize]
    public class OduncController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OduncController(UserManager<ApplicationUser> userManager,ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Kullanici")]
        public async Task<IActionResult> OduncAl(int kitapId)
        {
            var kullanici = await _userManager.Users
                .Include(x => x.Odunc)
                .SingleOrDefaultAsync(x => x.Id == _userManager.GetUserId(User));

            if (kullanici == null)
            {
                return RedirectToAction("GirisYap", "GirisYap");
            }
            else
            {
                var kitapVarMi = kullanici.Odunc.SingleOrDefault(x => x.KitapId == kitapId);

                if (kitapVarMi == null)
                {
                    var odunc = new Odunc
                    {
                        KitapId = kitapId,
                        KullaniciId = kullanici.Id,
                        OduncAlmaTarihi = DateTime.Now,
                    };

                    kullanici.Odunc.Add(odunc);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("OduncAldiginizKitaplar", "Odunc");
                }
                else
                {
                    return NotFound();
                }
            }
        }


        [HttpGet]
        [Authorize(Roles = "Kullanici")]
        public async Task<IActionResult> OduncAldiginizKitaplar()
        {
            var kullaniciId = _userManager.GetUserId(User);

            if(kullaniciId == null)
            {
                return RedirectToAction("GirisYap", "GirisYap");
            }
            else
            {
                var kullanicininOduncAldigiKitaplar = await _userManager.Users.Where(x => x.Id == kullaniciId)
                    .Include(x => x.Odunc)
                    .ThenInclude(x => x.Kitap)
                    .ToListAsync();

                return View(kullanicininOduncAldigiKitaplar);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Kullanici")]
        public async Task<IActionResult> IadeEt(int id)
        {
            var kullanici = await _userManager.Users
               .Include(x => x.Odunc)
               .SingleOrDefaultAsync(x => x.Id == _userManager.GetUserId(User));

            if (kullanici == null)
            {
                return RedirectToAction("GirisYap", "GirisYap");
            }
            else
            {
                var oduncAlinanKitap = kullanici.Odunc.SingleOrDefault(x => x.KitapId == id);

                if(oduncAlinanKitap == null)
                {
                    return NotFound();
                }
                else
                {
                    var iade = new Iade
                    {
                        OduncId = oduncAlinanKitap.OduncId,
                        KitapId = oduncAlinanKitap.KitapId,
                        KullaniciId = kullanici.Id,
                        IadeTarihi = DateTime.Now,
                    };

                    _context.Iade.Add(iade);
                    _context.Odunc.Remove(oduncAlinanKitap);
                    await _context.SaveChangesAsync();  

                    return RedirectToAction("IadeEttiginizKitaplar", "Odunc");
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Kullanici")]
        public async Task<IActionResult> IadeEttiginizKitaplar()
        {
            var kullaniciId = _userManager.GetUserId(User);

            if (kullaniciId == null)
            {
                return RedirectToAction("GirisYap", "GirisYap");
            }
            else
            {
                var iadeEdilenKitap = await _userManager.Users.Where(x => x.Id == kullaniciId)
                    .Include(x => x.Iade)
                    .ThenInclude(x => x.Kitap)
                    .ToListAsync();

                return View(iadeEdilenKitap);
            }
        }

    }
}
