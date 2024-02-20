using KutuphaneYonetimSistemi.Data;
using KutuphaneYonetimSistemi.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class YayinEviController : Controller
    {
        private readonly IGenericRepository<Yayinevi> _yayinEviRepo;

        public YayinEviController(IGenericRepository<Yayinevi> yayinEviRepo)
        {
            _yayinEviRepo = yayinEviRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> YayinEviListele()
        {
            var yayinEvleri = await _yayinEviRepo.GetAll();

            return View(yayinEvleri);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YayinEviEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YayinEviEkle(Yayinevi model)
        {
            if (ModelState.IsValid)
            {
                await _yayinEviRepo.Add(model);

                return RedirectToAction("YayinEviListele");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YayinEviGuncelle(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var yayinEvi = await _yayinEviRepo.GetById(id);

                if (yayinEvi == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(yayinEvi);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YayinEviGuncelle(int id, Yayinevi model)
        {
            var yayinEvleri = await _yayinEviRepo.GetById(id);

            if (yayinEvleri.YayinEviId != model.YayinEviId)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        yayinEvleri.YayinEviAdi = model.YayinEviAdi;
                        yayinEvleri.KurulusTarihi = model.KurulusTarihi;
                        yayinEvleri.Adres = model.Adres;

                        await _yayinEviRepo.Update(yayinEvleri);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction("YayinEviListele");
                }
                else
                {
                    return View(model);
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YayinEviSil(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var yayinEvi = await _yayinEviRepo.GetById(id);

            if (yayinEvi == null)
            {
                return NotFound();
            }
            else
            {
                return View(yayinEvi);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YayinEviSil(int id, Yayinevi model)
        {
            var yayinEvleri = await _yayinEviRepo.GetById(id);

            if (yayinEvleri.YayinEviId != model.YayinEviId)
            {
                return NotFound();
            }
            else
            {
                await _yayinEviRepo.Delete(id);
                return RedirectToAction("YayinEviListele");
            }
        }
    }
}
