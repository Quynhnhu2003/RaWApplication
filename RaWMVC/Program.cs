﻿using RaWMVC.Data;
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

builder.Services.AddDefaultIdentity<RaWMVCUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RaWIdentityContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    //=== Password settings. ===//
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    // số lượng ký tự đặc biệt
    options.Password.RequiredUniqueChars = 1;

    //=== Lockout settings.===//
    options.Lockout.MaxFailedAccessAttempts = 5; //số lượng đăng nhập thất bại
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    //=== User settings. ===//
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true; //được sử dụng email làm tên đăng nhập
});

builder.Services.ConfigureApplicationCookie(options =>
{
    //=== Cookie settings ===//
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});


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
app.UseAuthentication();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
