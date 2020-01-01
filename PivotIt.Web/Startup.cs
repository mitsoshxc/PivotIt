using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PivotIt.Infrastructure.Helpers;
using PivotIt.Infrastructure.Models;
using PivotIt.Infrastructure.Persistence;
using PivotIt.Search.Helpers;
using PivotIt.Web.Helpers;

namespace PivotIt.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.Configure<DatabaseOptions>(options => options.SiteConnectionString = Configuration.GetConnectionString("siteConnectionString"));

            services.AddDbContext<SiteContext>();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            //});

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login/");
                options.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Account/Logout/");
                options.Cookie.Name = "AuthenticationCookie";
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                //options.Events = new CookieAuthenticationEvents
                //{
                //    OnValidatePrincipal = context =>
                //    {
                //        var cookieValidatorService = context.HttpContext.RequestServices.GetRequiredService<ICookieValidatorService>();
                //        return cookieValidatorService.ValidateAsync(context);
                //    }
                //};
            });

            services.AddServerSideBlazor();

            services.DeclareServices();

            //add elastic search client
            var elasticConnectionString = Configuration.GetConnectionString("elasticSearch");
            services.DeclareElasticSearchClient(elasticConnectionString);
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

            // run pending migrations
            app.UpdateDatabase();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapBlazorHub();
            });
        }
    }
}
