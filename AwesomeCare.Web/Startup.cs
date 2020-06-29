using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.Services.Services;
using AwesomeCare.Web.AppSettings;
using AwesomeCare.Web.Middlewares;
using AwesomeCare.Web.Services.Admin;
using AwesomeCare.Web.Services.ClientRotaName;
using AwesomeCare.Web.Services.Communication;
using AwesomeCare.Web.Services.ShiftBooking;
using AwesomeCare.Web.Services.Staff;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Refit;

namespace AwesomeCare.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            //the AddAuthentication and AddCookie  must be same thing configured on the IdentityServer Project
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;//
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
               {
                 //  options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                   options.Cookie.Name = ".AwesomeCare.Cookie";
               })
               .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
               {
                   var settings = Configuration.GetSection("IDPClientSettings").Get<IDPClientSettings>();
                   options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                   options.Authority = Configuration["idp_url"].ToString();// "https://localhost:44392/";
                   options.ClientId = settings.ClientId;
                   options.ResponseType = "code";
                   //options.UsePkce = false;
                   //options.Scope.Add("openid");
                   //options.Scope.Add("profile");
                   //options.Scope.Add("offline_access");
                  // options.Scope.Add("awesomecareapi");
                   foreach (string scope in settings.Scopes)
                   {
                       options.Scope.Add(scope);
                   }
                   options.SaveTokens = true;
                   options.ClientSecret = settings.ClientSecret;
                   // options.SignedOutCallbackPath = "";
                  // options.AccessDeniedPath = "";
                 
                   options.GetClaimsFromUserInfoEndpoint = true;
                   //Remove Unnecessary claims
                   options.ClaimActions.DeleteClaim("s_hash");
                   options.ClaimActions.DeleteClaim("auth_time");
                   options.ClaimActions.DeleteClaim("sid");
                   options.ClaimActions.DeleteClaim("idp");

                   //Mapp Additional Claims as Configured in IProfileService in Identity Server Project
                   options.ClaimActions.MapUniqueJsonKey("hasStaffInfo", "hasStaffInfo");
                   options.ClaimActions.MapUniqueJsonKey(JwtClaimTypes.Role, JwtClaimTypes.Role);
                   options.ClaimActions.MapUniqueJsonKey(JwtClaimTypes.Email, JwtClaimTypes.Email);
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       NameClaimType = JwtClaimTypes.Name,
                       RoleClaimType = JwtClaimTypes.Role
                   };

                   options.Events.OnRedirectToIdentityProvider =async context =>
                   {
                       //https://developers.de/2019/11/29/how-to-pass-custom-parameters-in/
                   };
               });
            AutoMapperConfiguration.Configure();
            services.AddHttpContextAccessor();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddTransient<AuthenticatedHttpClientHandler>();
            services.AddScoped<HttpClient>();
            services.AddLogging();
            AddRefitServices(services);
            services.AddHttpClient("IdpClient", options =>
            {
                options.BaseAddress = new Uri(Configuration["idp_url"].ToString());
                options.DefaultRequestHeaders.Clear();
                options.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });
            services.AddMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();

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

            services.AddHttpClient("communicationService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICommunicationService>(r))
           .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
        }
    }
}
