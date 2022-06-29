using AFS.App.Providers;
using AFS.Logic;
using Microsoft.EntityFrameworkCore;

namespace AFS.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AFS.DatabaseModel.AFSDatabaseContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("AFSDatabase"), b => b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            });
            builder.Services.AddSingleton<IFunLanguageTranslationProvider, FunLanguageTranslationAPIProvider>();
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();


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

            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}