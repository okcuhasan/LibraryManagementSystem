using KutuphaneYonetimSistemi.Data;
using KutuphaneYonetimSistemi.Data.Context;
using KutuphaneYonetimSistemi.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KutuphaneYonetimSistemi.UI.Controllers
{
    public class KategoriController : Controller
    {
        private readonly IGenericRepository<Kategori> _kategoriRepo;
        private readonly ApplicationDbContext _context;

        public KategoriController(IGenericRepository<Kategori> kategoriRepo, ApplicationDbContext context)
        {
            _kategoriRepo = kategoriRepo;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> KategoriListele()
        {
            var kategoriler = await _kategoriRepo.GetAll();

            return View(kategoriler);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KategoriEkle(Kategori model)
        {
            if (ModelState.IsValid)
            {
                await _kategoriRepo.Add(model);

                return RedirectToAction("KategoriListele");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KategoriGuncelle(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var kategori = await _kategoriRepo.GetById(id);

                if (kategori == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(kategori);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KategoriGuncelle(int id, Kategori model)
        {
            var kategoriler = await _kategoriRepo.GetById(id);

            if (kategoriler.KategoriId != model.KategoriId)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        kategoriler.KategoriAdi = model.KategoriAdi;
                        kategoriler.Aciklama = model.Aciklama;

                        await _kategoriRepo.Update(kategoriler);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                    return RedirectToAction("KategoriListele");
                }
                else
                {
                    return View(model);
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KategoriSil(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var kategori = await _kategoriRepo.GetById(id);

            if (kategori == null)
            {
                return NotFound();
            }
            else
            {
                return View(kategori);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> KategoriSil(int id, Kategori model)
        {
            var kategoriler = await _kategoriRepo.GetById(id);

            if (kategoriler.KategoriId != model.KategoriId)
            {
                return NotFound();
            }
            else
            {
                await _kategoriRepo.Delete(id);
                return RedirectToAction("KategoriListele");
            }
        }

        


    }
}
