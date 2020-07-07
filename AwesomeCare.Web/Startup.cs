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
using Microsoft.Extensions.Logging;
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
        private ILogger<Startup> logger;
        string apipolicyname = "openidcookiepolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(logger =>
            {
                logger.AddConsole();
                logger.AddDebug();
             //   logger.AddAzureWebAppDiagnostics();
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(apipolicyname, policy =>
                {
                    policy.AddAuthenticationSchemes("OpenIdConnect");
                    policy.RequireAuthenticatedUser();

                });
            });

            //the AddAuthentication and AddCookie  must be same thing configured on the IdentityServer Project
            services.AddAuthentication("OpenIdConnect")
               .AddCookie("Identity.Application", options =>
               {
                   options.Events = new CookieAuthenticationEvents
                   {
                       OnRedirectToAccessDenied = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnRedirectToAccessDenied");
                           return Task.CompletedTask;
                       },
                       OnRedirectToLogin = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnRedirectToLogin");
                           return Task.CompletedTask;
                       },
                       OnRedirectToLogout = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnRedirectToLogout");
                           return Task.CompletedTask;
                       },
                       OnRedirectToReturnUrl = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnRedirectToReturnUrl");
                           return Task.CompletedTask;
                       },
                       OnSignedIn = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnSignedIn");
                           return Task.CompletedTask;
                       },
                       OnSigningOut = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnSigningOut");
                           return Task.CompletedTask;
                       },
                       OnValidatePrincipal = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnValidatePrincipal");
                           return Task.CompletedTask;
                       }
                   };
               })
               .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
               {
                   var settings = Configuration.GetSection("IDPClientSettings").Get<IDPClientSettings>();
                   options.SignInScheme = "Identity.Application";// CookieAuthenticationDefaults.AuthenticationScheme;
                   options.Authority = Configuration["idp_url"].ToString();// "https://localhost:44392/";
                   options.ClientId = settings.ClientId;
                   options.ResponseType = "code";
                   options.SignedOutRedirectUri = Configuration["app_url"];
                   foreach (string scope in settings.Scopes)
                   {
                       options.Scope.Add(scope);
                   }
                   options.SaveTokens = true;
                   options.ClientSecret = settings.ClientSecret;
                 
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
                   options.Events = new OpenIdConnectEvents
                   {
                       OnAccessDenied = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnAccessDenied");
                           return Task.CompletedTask;
                       },
                       OnAuthenticationFailed = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnAuthenticationFailed {ctx.Exception?.Message}");
                           return Task.CompletedTask;
                       },
                       OnAuthorizationCodeReceived = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation("OnAuthorizationCodeReceived");
                           return Task.CompletedTask;
                       },
                       OnMessageReceived = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation("OnMessageReceived");
                           return Task.CompletedTask;
                       },
                       OnRedirectToIdentityProvider = ctx =>
                       {
                           //https://developers.de/2019/11/29/how-to-pass-custom-parameters-in/
                           var tt = ctx;
                           logger.LogInformation("OnRedirectToIdentityProvider");
                           return Task.CompletedTask;
                       },
                       OnRedirectToIdentityProviderForSignOut = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation("OnRedirectToIdentityProviderForSignOut");
                           return Task.CompletedTask;
                       },
                       OnRemoteFailure = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation($"OnRemoteFailure {ctx.Failure?.Message}");
                           return Task.CompletedTask;
                       },
                       OnRemoteSignOut = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation("OnRemoteSignOut");
                           return Task.CompletedTask;
                       },
                       OnSignedOutCallbackRedirect = ctx =>
                       {
                           var tt = ctx;
                           
                           return Task.CompletedTask;
                       },
                       OnTicketReceived = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation("OnTicketReceived");
                           return Task.CompletedTask;
                       },
                       OnTokenResponseReceived = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation("OnTokenResponseReceived");
                           return Task.CompletedTask;
                       },
                       OnTokenValidated = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation("OnTokenValidated");
                           return Task.CompletedTask;
                       },
                       OnUserInformationReceived = ctx =>
                       {
                           var tt = ctx;
                           logger.LogInformation("OnUserInformationReceived");
                           return Task.CompletedTask;
                       }
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            this.logger = logger;
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
                endpoints.MapControllerRoute("default", "{controller=Staff}/{action=Profile}/{id?}").RequireAuthorization(apipolicyname);
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
