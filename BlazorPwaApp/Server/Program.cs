using BlazorPwaApp.Client.Services;
using BlazorPwaApp.Client.Services.UserAccountService;
using BlazorPwaApp.Server;
using BlazorPwaApp.Server.AppDbContext;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
   options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout duration
});

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMvc();
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddAuthentication("Cookies")
//    .AddCookie("Cookies", config =>
//    {
//       config.Cookie.Name = "__BlazorInfo__";
//       config.LoginPath = "/";
//       config.SlidingExpiration = true;
//    });

//builder.Services.AddSession();
//builder.Services.AddSession(options =>
//{
//   options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout duration
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseWebAssemblyDebugging();
}
else
{
   app.UseExceptionHandler("/Error");
   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
