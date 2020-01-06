using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.AutoMapperConfiguration;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Company;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using AwesomeCare.Admin.Middlewares;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientInvolvingParty;
using AwesomeCare.Admin.Services.ClientInvolvingPartyBase;
using AwesomeCare.Admin.Services.ClientRegulatoryContact;
using Microsoft.Extensions.FileProviders;
using System.IO;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.Services.RotaDayofWeek;
using AwesomeCare.Admin.Services.ClientRota;
using QRCoder;
using Dropbox.Api;
using AwesomeCare.Admin.Services.ClientCareDetails;

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
            services.AddScoped(typeof(QRCodeGenerator));
            services.AddScoped(typeof(DropboxClient),c=> new DropboxClient(Configuration["dropboxApiKey"]));
            //AutoMapper
            //AutoMapperConfig.Configure();
            MapperConfig.AutoMapperConfiguration.Configure();
            services.AddLogging();
            AddRefitServices(services);
            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseBaseRecordMiddleware();
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
            //app.UseStaticFiles(
            //    new StaticFileOptions
            //    {
            //        FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath,"Uploads")),
            //        RequestPath = new PathString("/Files")
            //    }
            //    );
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        void AddRefitServices(IServiceCollection services)
        {
            string uri = Configuration["AwesomeCareBaseApi"];
            services.AddRefitClient<ICompanyService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(uri));
            services.AddRefitClient<ICompanyContactService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(uri));
            services.AddRefitClient<IBaseRecordService>()
               .ConfigureHttpClient(c => c.BaseAddress = new Uri(uri));
            //services.AddRefitClient<IClientService>()
            //  .ConfigureHttpClient(c => c.BaseAddress = new Uri(uri));

            services.AddHttpClient("clientservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientService>(r));
            services.AddHttpClient("clientserviceparty", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientInvolvingParty>(r));

            services.AddHttpClient("clientservicepartybase", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientInvolvingPartyBase>(r));

            services.AddHttpClient("clientregulatorycontactservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRegulatoryContactService>(r));

            services.AddHttpClient("clientrotanameservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaNameService>(r));

            services.AddHttpClient("clientrotaservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaService>(r));

            
            services.AddHttpClient("clientrotatypeservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaTypeService>(r));

            services.AddHttpClient("clientrotataskservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IRotaTaskService>(r));


            services.AddHttpClient("rotadayofweekservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IRotaDayofWeekService>(r));

            services.AddHttpClient("clientcaredetails", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientCareDetails>(r));
            
        }
    }
}
