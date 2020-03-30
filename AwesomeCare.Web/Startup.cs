using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.Services.Services;
using AwesomeCare.Web.Middlewares;
using AwesomeCare.Web.Services.Admin;
using AwesomeCare.Web.Services.ClientRotaName;
using AwesomeCare.Web.Services.ShiftBooking;
using AwesomeCare.Web.Services.Staff;

using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
               
            })
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,options=> {
                   
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                   options.Cookie.Name = ".AwesomeCareWeb.Cookie";
                   
               })
               .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
               {
                   options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.Authority = "https://localhost:44392/";
                    options.ClientId = "awesomecareweb";
                   options.ResponseType = "code";
                    //options.UsePkce = false;
                    options.Scope.Add("openid");
                   options.Scope.Add("profile");
                   options.SaveTokens = true;
                   options.ClientSecret = "1234567890";
                   // options.SignedOutCallbackPath = "";
                   options.Scope.Add("awesomecareapi");
                  
                    options.GetClaimsFromUserInfoEndpoint = true;
               });
            AutoMapperConfiguration.Configure();
            services.AddHttpContextAccessor();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddTransient<AuthenticatedHttpClientHandler>();
            services.AddScoped<HttpClient>();
            services.AddLogging();
            AddRefitServices(services);
            services.AddMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,IWebHostEnvironment  env)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
                      
            app.UseCookiePolicy();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=ShiftBooking}/{action=Shifts}/{id?}").RequireAuthorization();
            });
            app.UseBaseRecordMiddleware();
        }

        void AddRefitServices(IServiceCollection services)
        {
            string uri = Configuration["AwesomeCareBaseApi"];
           
           

            services.AddHttpClient("baserecordservice", c =>
            {
              //  var serviceBusPersisterConnection = ServiceProviderServiceExtensions.GetService<IHttpContextAccessor>();
                c.BaseAddress = new Uri(uri);
               // c.SetBearerToken
            }).AddTypedClient(r => RestService.For<IBaseRecordService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("shiftbookingservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IShiftBookingService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientrotanameservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaNameService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
            
        }
    }
}
