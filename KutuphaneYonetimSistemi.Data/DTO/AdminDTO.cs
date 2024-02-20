using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data.DTO
{
    public class AdminDTO
    {
        [Required(ErrorMessage = "Admin kullanıcı adı zorunludur!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur!")]
        public string Sifre { get; set; }


        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor!")]
        [Required(ErrorMessage = "Şifre tekrar alanı zorunludur!")]
        public string SifreTekrar { get; set; }
    }
}
