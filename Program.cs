using LoadsheddingV1.Controllers;
using LoadsheddingV1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoadsheddingV1.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LoadsheddingV1ContextConnection") ?? throw new InvalidOperationException("Connection string 'LoadsheddingV1ContextConnection' not found.");
var loadSheddingConnectionString = builder.Configuration.GetConnectionString("LoadSheddingContextConnection") ?? throw new InvalidOperationException("Connection string 'LoadSheddingContextConnection' not found.");

builder.Services.AddDbContext<LoadsheddingV1Context>(options =>
    options.UseSqlServer(connectionString));

// Add LoadSheddingContext with LocalDB connection
builder.Services.AddDbContext<LoadSheddingContext>(options =>
    options.UseSqlServer(loadSheddingConnectionString));

//builder.Services.AddDefaultIdentity<LoadsheddingV1User>(options => options.SignIn.RequireConfirmedAccount = true)
  //  .AddEntityFrameworkStores<LoadsheddingV1Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HttpClient service
builder.Services.AddHttpClient();

// Add LoadSheddingUpdateService as a hosted service
builder.Services.AddHostedService<LoadSheddingUpdateService>();





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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
