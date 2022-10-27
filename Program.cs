using HotelService1;
using HotelService1.Models;
using HotelService1.Repositories;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

namespace test
{
    public class HotelService1
    {
        public static void Main(string[] args)
        {
            NLog.LogManager.Setup().LoadConfigurationFromAppSettings();
            //logger.Debug("init main");

            ////try
            ////{
            //    var builder = WebApplication.CreateBuilder(args);

            //    // Add services to the container.
            //    builder.Services.AddControllersWithViews();

            //    // NLog: Setup NLog for Dependency injection
            //    builder.Logging.ClearProviders();
            //    builder.Host.UseNLog();
            //builder.Services.AddSwaggerGen();
            //var app = builder.Build();

            //    // Configure the HTTP request pipeline.
            //    if (!app.Environment.IsDevelopment())
            //    {
            //        app.UseExceptionHandler("/Home/Error");
            //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //        app.UseHsts();
            //    }
            //else
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //    app.UseHttpsRedirection();
            //    app.UseStaticFiles();

            //    app.UseRouting();

            //    app.UseAuthorization();

            //    app.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");

            //    app.Run();
            //}
            //catch (Exception exception)
            //{
            //    // NLog: catch setup errors
            //    logger.Error(exception, "Stopped program because of exception");
            //    throw;
            //}
            //finally
            //{
            //    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            //    NLog.LogManager.Shutdown();
            //}
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseNLog();   
    }
}

