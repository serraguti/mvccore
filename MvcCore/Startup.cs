using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Repositories;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
            services.AddResponseCaching();


            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            });

            String cadenasql =
                this.Configuration.GetConnectionString("cadenasqlhospitalcasa");
            String cadenaoracle =
                this.Configuration.GetConnectionString("cadenaoracle");
            String cadenamysql =
                this.Configuration.GetConnectionString("cadenamysql");
            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddSingleton<MailService>();
            services.AddSingleton<UploadService>();
            services.AddSingleton<PathProvider>();

            services.AddTransient<RepositoryUsuarios>();
            services.AddTransient<RepositoryJoyerias>();
            services.AddTransient<RepositoryAlumnos>();
            services.AddTransient<IRepositoryDepartamentos
            , RepositoryDepartamentosSQL>();
            services.AddTransient<IRepositoryHospital, RepositoryHospital>();
            services.AddDbContext<HospitalContext>(options =>
            options.UseSqlServer(cadenasql));
            //services.AddTransient<IRepositoryDepartamentos>(x =>
            //new RepositoryDepartamentosOracle(cadenaoracle));
            //services.AddTransient<IRepositoryDepartamentos,
            //    RepositoryDepartamentosMySql>();
            //services.AddDbContext<HospitalContext>(options =>
            //options.UseMySql(cadenamysql, new MySqlServerVersion(new Version(8, 0, 22))
            //, mySqlOptions => 
            //mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
            //services.AddDbContextPool<HospitalContext>
            //    (options => options.UseMySql(cadenamysql, ServerVersion.AutoDetect(cadenamysql)));

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
            app.UseResponseCaching();

            app.UseSession();
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
