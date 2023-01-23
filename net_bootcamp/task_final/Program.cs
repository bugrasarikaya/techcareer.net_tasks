using Microsoft.AspNetCore.Authentication.Cookies;
namespace task_final {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();
			builder.Services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.Cookie.Name = "Authentication"; options.Cookie.HttpOnly = true; options.LoginPath = new PathString("/Login/Main"); options.LogoutPath = new PathString("/Login/Main"); options.AccessDeniedPath = new PathString("/Login/Main"); options.ExpireTimeSpan = TimeSpan.FromMinutes(30); options.SlidingExpiration = false; });
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options => { options.Cookie.Name = "AccountID"; options.IdleTimeout = TimeSpan.FromMinutes(30); options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true; });
			var app = builder.Build();
			if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Login/Error");
			app.UseStaticFiles();
			app.UseRouting();
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseSession();
			app.MapControllerRoute(name: "default", pattern: "{controller=Login}/{action=Main}/{id?}");
			app.Run();
		}
	}
}