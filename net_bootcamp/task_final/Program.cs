using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace task_final {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.Cookie.Name = "authentication";
                    options.Cookie.HttpOnly = true;
                    options.LoginPath = new PathString("/Login/Main");
                    options.LogoutPath = new PathString("/Login/Main");
                    options.AccessDeniedPath = new PathString("/Home/Main");
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.SlidingExpiration = false;
                });
            var app = builder.Build();
            if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Login/Error");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(name: "default", pattern: "{controller=Login}/{action=Main}/{id?}");
            app.Run();
        }
    }
}