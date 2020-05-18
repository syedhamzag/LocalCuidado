using System;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Company;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using AwesomeCare.Admin.Middlewares;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientInvolvingParty;
using AwesomeCare.Admin.Services.ClientInvolvingPartyBase;
using AwesomeCare.Admin.Services.ClientRegulatoryContact;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.Services.RotaDayofWeek;
using AwesomeCare.Admin.Services.ClientRota;
using QRCoder;
using Dropbox.Api;
using AwesomeCare.Admin.Services.ClientCareDetails;
using AwesomeCare.Services.Services;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffCommunication;
using AwesomeCare.Admin.Services.Untowards;
using AwesomeCare.Admin.Services.ShiftBooking;
using AwesomeCare.Admin.Services.StaffWorkTeam;
using Microsoft.Extensions.Hosting;
using AwesomeCare.Admin.Services.Medication;

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
            services.AddScoped<IFileUpload, FileUpload>();
            //AutoMapper
            AutoMapperConfiguration.Configure();
          //  MapperConfig.AutoMapperConfiguration.Configure();
            services.AddLogging();
            AddRefitServices(services);
            // services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession(options=> {
                options.Cookie.Name = ".Awesomecare.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews()
                .AddSessionStateTempDataProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseBaseRecordMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     "{controller=Client}/{action=HomeCare}/{id?}");
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

            services.AddHttpClient("staffservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffService>(r));

            services.AddHttpClient("staffcommunication", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffCommunication>(r));

            services.AddHttpClient("untowardsService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IUntowardsService>(r));

            services.AddHttpClient("shiftbookingservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IShiftBookingService>(r));

            services.AddHttpClient("staffworkteamservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffWorkTeamService>(r));

            services.AddHttpClient("medicationservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IMedicationService>(r));

            
        }
    }
}
