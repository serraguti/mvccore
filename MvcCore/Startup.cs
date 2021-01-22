using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Repositories;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            String cadenasql =
                this.Configuration.GetConnectionString("cadenasqlhospitalcasa");
            String cadenaoracle =
                this.Configuration.GetConnectionString("cadenaoracle");
            services.AddTransient<PathProvider>();
            services.AddTransient<RepositoryJoyerias>();
            services.AddTransient<RepositoryAlumnos>();
            //services.AddTransient<IRepositoryDepartamentos, RepositoryDepartamentosSQL>();
            services.AddTransient<IRepositoryDepartamentos>(x =>
            new RepositoryDepartamentosOracle(cadenaoracle));
            services.AddDbContext<DepartamentosContext>(options =>
            options.UseSqlServer(cadenasql));
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app
            , IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
