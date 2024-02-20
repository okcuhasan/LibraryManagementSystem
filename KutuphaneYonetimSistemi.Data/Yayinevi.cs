using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class Yayinevi
    {
        [Key]
        public int YayinEviId { get; set; }
        [Required(ErrorMessage = "Yayınevi Adı alanı boş geçilemez!")]
        public string YayinEviAdi { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? KurulusTarihi { get; set; }
        [Required(ErrorMessage = "Adres alanı boş geçilemez!")]
        public string Adres { get; set; }

        public List<Kitap> ?Kitaplar { get; set; }
    }
}
