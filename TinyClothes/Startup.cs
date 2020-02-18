using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinyClothes.Data;

namespace TinyClothes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //private void ConfigDbContext(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer("con goes here");
        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            string connection = Configuration.GetConnectionString("ClothesDB");

            //services.AddDbContext<StoreContext>(ConfigDbContext);
            // same as above using Lamba notation.
            services.AddDbContext<StoreContext>
                (
                    options => options.UseSqlServer(connection)
                );

            // Where session data is going to be stored
            services.AddDistributedMemoryCache();

            // Add & configure session for state managment
            services.AddSession(options =>
            {
                // Any name we choose, it's just for developers, not for the user.
                options.Cookie.Name = ".TinyClothes.Session";

                // how long session lasts
                options.IdleTimeout = TimeSpan.FromMinutes(20.0);

                // GDPR; European Privacy
                // Session cookies always get created even if user does not acept cookie policy.
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Allows session to be accessed
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
