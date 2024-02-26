using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class YorumCevap
    {
        [Key]
        public int CevapId { get; set; }    

        public string? CevapIcerigi { get; set; }

        public int? YorumId { get; set; }
        public Yorum? Yorum { get; set; }

        public string? KullaniciId { get; set; }
        public ApplicationUser? Kullanici { get; set; }
    }
    
}
