using KutuphaneYonetimSistemi.Data;
using KutuphaneYonetimSistemi.Data.Context;
using KutuphaneYonetimSistemi.Service.Abstract;
using KutuphaneYonetimSistemi.Service.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KutuphaneYonetimiConnectionString"));
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<IGenericRepository<Kitap>, GenericRepository<Kitap>>();
builder.Services.AddScoped<IGenericRepository<Yazar>, GenericRepository<Yazar>>();
builder.Services.AddScoped<IGenericRepository<Yayinevi>, GenericRepository<Yayinevi>>();
builder.Services.AddScoped<IGenericRepository<Kategori>, GenericRepository<Kategori>>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Kullanici" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
