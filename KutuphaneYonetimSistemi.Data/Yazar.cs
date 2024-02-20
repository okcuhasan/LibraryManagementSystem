using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class Yazar
    {
        [Key]
        public int YazarId { get; set; }
        [Required(ErrorMessage = "Yazar Ad Soyad alanı boş geçilemez!")]
        public string YazarAdiSoyadi { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? DogumTarihi { get; set; }

        public List<Kitap> ?Kitaplar { get; set; }
    }
}
