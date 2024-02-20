using KutuphaneYonetimSistemi.Data;
using KutuphaneYonetimSistemi.Data.Context;
using KutuphaneYonetimSistemi.Data.Migrations;
using KutuphaneYonetimSistemi.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class KitapController : Controller
    {
        private readonly IGenericRepository<Kitap> _kitapRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        public KitapController(IGenericRepository<Kitap> kitapRepo, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _kitapRepo = kitapRepo;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> KitapListele()
        {
            var kitaplar = await _context.Kitaplar.Include(x => x.Yazar)
                .Include(x => x.Yayinevi)
                .Include(x => x.Kategori)
                .Include(x => x.Yorumlar)
                .ThenInclude(x => x.Kullanici)
                .ToListAsync();

            return View(kitaplar);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KitapEkle()
        {
            ViewBag.Yazarlar = new SelectList(await _context.Yazarlar.ToListAsync(), "YazarId", "YazarAdiSoyadi");
            ViewBag.YayinEvleri = new SelectList(await _context.Yayinevleri.ToListAsync(), "YayinEviId", "YayinEviAdi");
            ViewBag.Kategoriler = new SelectList(await _context.Kategoriler.ToListAsync(), "KategoriId", "KategoriAdi");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KitapEkle(Kitap model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads"); 
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName; 
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName); 

                    using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                    {
                        file.CopyTo(fileStream); 
                    }

                    model.ImagePath = "/uploads/" + uniqueFileName; 
                }
                await _kitapRepo.Add(model);
                return RedirectToAction("KitapListele");
            }
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KitapGuncelle(int id)
        {
            ViewBag.Yazarlar = new SelectList(await _context.Yazarlar.ToListAsync(), "YazarId", "YazarAdiSoyadi");
            ViewBag.YayinEvleri = new SelectList(await _context.Yayinevleri.ToListAsync(), "YayinEviId", "YayinEviAdi");
            ViewBag.Kategoriler = new SelectList(await _context.Kategoriler.ToListAsync(), "KategoriId", "KategoriAdi");

            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var kitap = await _kitapRepo.GetById(id);

                if(kitap == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(kitap);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KitapGuncelle(int id, Kitap model, IFormFile? file)
        {
            var kitap = await _kitapRepo.GetById(id);

            if (kitap.KitapId != model.KitapId)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (file != null && file.Length > 0)
                        {
                            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }

                            model.ImagePath = "/uploads/" + uniqueFileName;
                        }

                        kitap.KitapAdi = model.KitapAdi;
                        kitap.KitapFiyati = model.KitapFiyati;
                        kitap.KitapYayinTarihi = model.KitapYayinTarihi;
                        kitap.SayfaSayisi = model.SayfaSayisi;
                        kitap.Dil = model.Dil;
                        kitap.ImagePath = model.ImagePath;
                        kitap.YazarId = model.YazarId;
                        kitap.YayineviId = model.YayineviId;
                        kitap.KategoriId = model.KategoriId;

                        await _kitapRepo.Update(kitap);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }

                    return RedirectToAction("KitapListele");
                }
                else
                {
                    return View(model);
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KitapSil(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            else
            {
                var kitap = await _kitapRepo.GetById(id);

                if(kitap == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(kitap);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KitapSil(int id,Kitap model)
        {
            var kitap = await _kitapRepo.GetById(id);

            if(kitap.KitapId != model.KitapId)
            {
                return NotFound();
            }
            else
            {
                await _kitapRepo.Delete(id);
                return RedirectToAction("KitapListele");
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> KitapArama()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> KitapArama(string kitapVeyaYazarAdi)
        {
            var kitaplar = await _context.Kitaplar
                .Include(x => x.Yazar)
                .Include(x => x.Yayinevi)
                .Where(x => x.KitapAdi == kitapVeyaYazarAdi || x.Yazar.YazarAdiSoyadi == kitapVeyaYazarAdi).ToListAsync();

            if(kitaplar == null)
            {
                return NotFound();
            }
            else
            {
                return View(kitaplar);
            }
        }
    }
}