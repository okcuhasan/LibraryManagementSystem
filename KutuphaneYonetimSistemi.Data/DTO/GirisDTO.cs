using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data.DTO
{
    public class GirisDTO
    {
        [Required(ErrorMessage = "Kullanıcı Adı alanı boş geçilemez!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş geçilemez!")]
        public string Sifre { get; set; }
    }
}
