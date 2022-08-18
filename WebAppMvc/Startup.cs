using Microsoft.EntityFrameworkCore;
using WebAppMvc.Middlewares;
using WebAppMvc.Models;

namespace WebAppMvc
{
    public class Startup
    {
        public static IWebHostEnvironment _env;
        public static IConfiguration _configuration;
        public Startup(IWebHostEnvironment env,IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }
        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{_env.ApplicationName}- ASP.Net Core tutoral project");
            });
        }
        private static void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {_env.ApplicationName}. App running configuration: {_env.EnvironmentName}");
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
            services.AddControllersWithViews();
            services.AddSingleton<IBlogRepository, BlogRepository>();
            

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine($"Launching project from: {env.ContentRootPath}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseMiddleware<LoggingMiddleware>();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=index}/{id}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!"); });
            });

            app.Map("/about", About);
            app.Map("/config", Config);

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/config", async zapis => { await zapis.Response.WriteAsync($" App Name {env.ApplicationName}. App running conf {env.EnvironmentName}"); });
            //});



            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Page not found");
            });
        }

    }
}
