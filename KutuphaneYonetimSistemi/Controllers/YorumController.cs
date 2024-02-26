using KutuphaneYonetimSistemi.Data;
using KutuphaneYonetimSistemi.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class YorumController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public YorumController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost]
        [Authorize(Roles = "Kullanici")]
        public async Task<JsonResult> YorumEkle(int kitapId,string yorumIcerigi)
        {
            try
            {
                var kullaniciId = _userManager.GetUserId(User);

                var yorum = new Yorum
                {
                    YorumIcerigi = yorumIcerigi,
                    KitapId = kitapId,
                    KullaniciId = kullaniciId,
                };
                _context.Yorumlar.Add(yorum);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Yorum başarı ile eklendi!" });
            }
            catch
            {
                return Json(new { success = false, message = "Yorum eklenirken bir hata oluştu!" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Kullanici")]
        public async Task<JsonResult> CevapEkle(int yorumId,string cevapIcerigi)
        {
            try
            {
                var kullaniciId = _userManager.GetUserId(User);

                var yorum = _context.Yorumlar.Find(yorumId);

                if(yorum == null)
                {
                    return Json(new { success = false, message = "Yorum bulunamadı!" });
                }
                
                if(yorum.KullaniciId == kullaniciId)
                {
                    return Json(new { success = false, message = "Kendi yorumunuza cevap ekleyemezsiniz!" });
                }

                var cevap = new YorumCevap
                {
                    CevapIcerigi = cevapIcerigi,
                    YorumId = yorumId,
                    KullaniciId = kullaniciId,
                };
                
                _context.YorumCevaplari.Add(cevap);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cevabınız başarı ile eklendi!" });
            }
            catch 
            {
                return Json(new { success = false, message = "Cevap eklenirken bir hata oluştu!" });
            }
        }
    }
}
