using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Ad { get; set; }

        public string? SoyAd { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş geçilemez!")]
        public string Sifre { get; set; }

        public string? EMailAdresi { get; set; }


        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor!")]
        [Required(ErrorMessage = "Şifre tekrar alanı zorunludur!")]
        public string SifreTekrar { get; set; }

        public List<Odunc> Odunc { get; set; } = new List<Odunc>();

        public List<Iade> Iade { get; set; } = new List<Iade>();

        public List<Yorum> Yorumlar { get; set; } = new List<Yorum>();

        public List<YorumCevap> YorumCevaplari { get; set; } = new List<YorumCevap>();
    }
}
