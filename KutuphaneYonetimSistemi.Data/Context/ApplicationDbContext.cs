using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Kitap> Kitaplar { get; set; }

        public DbSet<Yayinevi> Yayinevleri { get; set; }

        public DbSet<Yazar> Yazarlar { get; set; }

        public DbSet<Odunc> Odunc { get; set; }

        public DbSet<Iade> Iade { get; set; }

        public DbSet<Kategori> Kategoriler { get; set; }

        public DbSet<Yorum> Yorumlar { get; set; }

        public DbSet<YorumCevap> YorumCevaplari { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
            base.OnConfiguring(optionsBuilder);
        }

    }
}
