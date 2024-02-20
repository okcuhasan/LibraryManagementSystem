using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class Odunc
    {
        [Key]
        public int OduncId { get; set; }

        public int? KitapId { get; set; }
        public Kitap? Kitap { get; set;}

        public string? KullaniciId { get; set; }

        public ApplicationUser? Kullanici { get; set; }

        public DateTime? OduncAlmaTarihi { get; set; }

        public List<Iade> Iade { get; set; } = new List<Iade>();
    }
}
