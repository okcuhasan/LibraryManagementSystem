using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class Kitap
    {
        [Key]
        public int KitapId { get; set; }

        [Required(ErrorMessage = "Kitap Adı alanı boş geçilemez!")]
        public string KitapAdi { get; set; }
        public string? ImagePath { get; set; }
        [Required(ErrorMessage = "Kitap Fiyatı alanı boş geçilemez!")]
        public decimal KitapFiyati { get; set; }
        [Required(ErrorMessage = "Kitap Yayınlanma Tarihi alanı boş geçilemez!")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime KitapYayinTarihi { get; set; }
        [Required(ErrorMessage = "Kitap Sayfa Sayısı alanı boş geçilemez!")]
        public string SayfaSayisi { get; set; }
        [Required(ErrorMessage = "Kitap Dil alanı boş geçilemez!")]
        public string Dil { get; set; }

        public int? YazarId { get; set; }
        public Yazar? Yazar { get; set; }

        public int? YayineviId { get; set; }
        public Yayinevi? Yayinevi { get; set; }

        public int? KategoriId { get; set; }
        public Kategori? Kategori { get; set; }

        public List<Odunc> Odunc { get; set; } = new List<Odunc>();

        public List<Iade> Iade { get; set; } = new List<Iade>();

        public List<Yorum> Yorumlar { get; set; } = new List<Yorum>();
    }
}
