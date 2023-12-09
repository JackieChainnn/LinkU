using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LinkU.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using LinkU.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using QRCoder;
using LinkU.Interfaces;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options =>
    options.Conventions.AuthorizePage("/Admin/Index"));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ISupportManager, SupportManager>();
builder.Services.AddSingleton(new QRCodeService(new QRCodeGenerator()));

// Set applicationUser email to be unique
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

// TODO: Add custom authorization policies
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();

/* 
* Map areas by user role
* Employee for company area
* User for default area
*/
app.MapAreaControllerRoute(
    name: "Company",
    areaName: "Company",
    pattern: "Company/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
