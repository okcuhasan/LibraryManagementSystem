using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class Yorum
    {
        [Key]
        public int YorumId { get; set; }

        public string? YorumIcerigi { get; set; }

        public int? KitapId { get; set; }
        public Kitap? Kitap { get; set; }

        public string? KullaniciId { get; set; }
        public ApplicationUser? Kullanici { get; set; }

        public List<YorumCevap> YorumCevaplari { get; set; } = new List<YorumCevap>();
    }
}
