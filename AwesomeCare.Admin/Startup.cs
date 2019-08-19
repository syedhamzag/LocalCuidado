using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.AutoMapperConfiguration;
using AwesomeCare.Admin.Services.Company;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AwesomeCare.Admin
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //AutoMapper
            AutoMapperConfig.Configure();
            AddRefitServices(services);
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        void AddRefitServices(IServiceCollection services)
        {
            services.AddRefitClient<ICompanyService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["AwesomeCareBaseApi"]));
            services.AddRefitClient<ICompanyContactService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["AwesomeCareBaseApi"]));
        }
    }
}
