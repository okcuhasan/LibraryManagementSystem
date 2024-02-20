using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }
        [Required(ErrorMessage = "Kategori Adı alanı boş geçilemez!")]
        public string KategoriAdi { get; set; }

        public string? Aciklama { get; set; }

        public List<Kitap>? Kitaplar { get; set; }
    }
}
