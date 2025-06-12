using DinkToPdf.Contracts;
using DinkToPdf;
using FinalBestBrightnessStore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
/*builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});*/

/*string wkHtmlToPdfPath = Environment.GetEnvironmentVariable("PATH_TO_WKHTMLTOPDF");

if (string.IsNullOrEmpty(wkHtmlToPdfPath))
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        wkHtmlToPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wkhtmltox", "bin", "wkhtmltox.dll");
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        wkHtmlToPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wkhtmltox", "wkhtmltox.so");
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        wkHtmlToPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wkhtmltox", "wkhtmltox.dylib");
    }
    else
    {
        throw new PlatformNotSupportedException("The current OS platform is not supported.");
    }
}*/

// Custom assembly load context to load native library for DinkToPdf
//var context = new CustomAssemblyLoadContext();
//context.LoadUnmanagedLibrary(wkHtmlToPdfPath);


// Register DinkToPdf service
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


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

app.UseSession(); // Enable session management
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
