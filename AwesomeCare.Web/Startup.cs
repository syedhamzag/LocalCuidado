using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Services.Services;
using AwesomeCare.Web.Middlewares;
using AwesomeCare.Web.Services.Admin;
using AwesomeCare.Web.Services.ClientRotaName;
using AwesomeCare.Web.Services.ShiftBooking;
using AwesomeCare.Web.Services.Staff;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
namespace AwesomeCare.Web
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

          AutoMapperConfiguration.Configure();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddLogging();
            AddRefitServices(services);
            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
         //   services.AddRefitClient()
         //.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.example.com"));
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
            app.UseBaseRecordMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ShiftBooking}/{action=Shifts}/{id?}");
            });
        }

        void AddRefitServices(IServiceCollection services)
        {
            string uri = Configuration["AwesomeCareBaseApi"];
           
           

            services.AddHttpClient("baserecordservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IBaseRecordService>(r));

            services.AddHttpClient("staffservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffService>(r));

            services.AddHttpClient("shiftbookingservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IShiftBookingService>(r));

            services.AddHttpClient("clientrotanameservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaNameService>(r));
            
        }
    }
}
