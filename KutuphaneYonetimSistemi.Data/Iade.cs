using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class Iade
    {
        [Key]
        public int IadeId { get; set; }

        public int? OduncId { get; set; }
        public Odunc? Odunc { get; set;}

        public int? KitapId { get; set; }
        public Kitap? Kitap { get; set; }

        public string? KullaniciId { get; set; }

        public ApplicationUser? Kullanici { get; set; }

        public DateTime? IadeTarihi { get; set; }
    }
}
