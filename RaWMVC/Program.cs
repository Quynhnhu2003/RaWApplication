using RaWMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RaWMVCContextConnection") ?? throw new InvalidOperationException("Connection string 'RaWMVCContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

// AddContext
builder.Services.AddDbContext<RaWDbContext>();
builder.Services.AddDbContext<RaWIdentityContext>();

builder.Services.AddDefaultIdentity<RaWMVCUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<RaWIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
