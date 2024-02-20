using KutuphaneYonetimSistemi.Data;
using KutuphaneYonetimSistemi.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class YazarController : Controller
    {
        private readonly IGenericRepository<Yazar> _yazarRepo;

        public YazarController(IGenericRepository<Yazar> yazarRepo)
        {
            _yazarRepo = yazarRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> YazarListele()
        {
            var yazarlar = await _yazarRepo.GetAll();  

            return View(yazarlar);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YazarEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YazarEkle(Yazar model)
        {
            if(ModelState.IsValid)
            {
                await _yazarRepo.Add(model);

                return RedirectToAction("YazarListele");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YazarGuncelle(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var yazar = await _yazarRepo.GetById(id);

                if (yazar == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(yazar);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YazarGuncelle(int id, Yazar model)
        {
            var yazarlar = await _yazarRepo.GetById(id);

            if (yazarlar.YazarId != model.YazarId)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        yazarlar.YazarAdiSoyadi = model.YazarAdiSoyadi;
                        yazarlar.DogumTarihi = model.DogumTarihi;

                        await _yazarRepo.Update(yazarlar);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction("YazarListele");
                }
                else
                {
                    return View(model);
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YazarSil(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var yazar = await _yazarRepo.GetById(id);

            if (yazar == null)
            {
                return NotFound();
            }
            else
            {
                return View(yazar);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> YazarSil(int id, Yazar model)
        {
            var yazarlar = await _yazarRepo.GetById(id);

            if (yazarlar.YazarId != model.YazarId)
            {
                return NotFound();
            }
            else
            {
                await _yazarRepo.Delete(id);
                return RedirectToAction("YazarListele");
            }
        }

    }

}
