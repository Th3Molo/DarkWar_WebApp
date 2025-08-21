using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        var cultureInfo = new CultureInfo("de-DE");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Index";
            options.LogoutPath = "/Logout";
        });

        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/");
            options.Conventions.AllowAnonymousToPage("/Index");
            options.Conventions.AllowAnonymousToPage("/Registration");
        });

        // Session aktivieren
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddHttpContextAccessor();

        // DB-Context
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Logging
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();


        // Identity für .NET 8 konfigurieren
        builder.Services.AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
            .AddRoles<IdentityRole>()                     // optional, falls du Rollen brauchst
            .AddEntityFrameworkStores<AppDbContext>()     // DB-Backend
            .AddSignInManager()                           // wichtig für Login
            .AddDefaultTokenProviders();

        // Identity UI & Cookies aktivieren
        builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Migrationen anwenden
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        // Pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}