using Microsoft.EntityFrameworkCore;
using RestoManager_X.Models.RestosModel;

var builder = WebApplication.CreateBuilder(args);

// ✅ Ajouter tous les services ici AVANT builder.Build()
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RestosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestosConnection")));

var app = builder.Build();

// 🔧 Configurer le pipeline HTTP ici
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Restaurants}/{action=Index}/{id?}");

app.Run();
