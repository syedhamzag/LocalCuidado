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
using AwesomeCare.Admin.Services.StaffBlackLIst;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using AwesomeCare.Admin.AppSettings;
using System.IdentityModel.Tokens.Jwt;
using AwesomeCare.Admin.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AwesomeCare.Admin.Services.IncidentReport;
using AwesomeCare.Admin.Services.Investigation;

namespace AwesomeCare.Admin
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
        const string apipolicyname = "openidcookiepolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging(logger =>
            {
                logger.AddConsole();
                logger.AddDebug();
                logger.AddAzureWebAppDiagnostics();
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });


            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;//
            //    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            //})
            //   .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //   {
            //       //  options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //       options.Cookie.Name = ".AwesomeCare.Cookie";
            //       options.Events = new CookieAuthenticationEvents
            //       {
            //           OnRedirectToAccessDenied = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnRedirectToAccessDenied");
            //                return Task.CompletedTask;
            //            },
            //           OnRedirectToLogin = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnRedirectToLogin");
            //                return Task.CompletedTask;
            //            },
            //           OnRedirectToLogout = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnRedirectToLogout");
            //                return Task.CompletedTask;
            //            },
            //           OnRedirectToReturnUrl = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnRedirectToReturnUrl");
            //                return Task.CompletedTask;
            //            },
            //           OnSignedIn = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnSignedIn");
            //                return Task.CompletedTask;
            //            },
            //           OnSigningOut = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnSigningOut");
            //                return Task.CompletedTask;
            //            },
            //           OnValidatePrincipal = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnValidatePrincipal");
            //                return Task.CompletedTask;
            //            }
            //       };
            //   })

            services.AddAuthorization(options =>
            {
                options.AddPolicy(apipolicyname, policy =>
                {
                    policy.AddAuthenticationSchemes("OpenIdConnect", "Identity.Application");
                    policy.RequireAuthenticatedUser();

                });
            });
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
                   options.SignInScheme = "Identity.Application";//  CookieAuthenticationDefaults.AuthenticationScheme;
                   options.Authority = Configuration["idp_url"].ToString();
                   options.ClientId = settings.ClientId;
                   options.ResponseType = "code";
                   options.SignedOutRedirectUri = Configuration["app_url"];// + "/signout-callback-oidc";
                  // options.SignedOutCallbackPath = new PathString("/signout-callback-oidc");
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
                   // options.ClaimActions.MapUniqueJsonKey("hasStaffInfo", "hasStaffInfo");
                   options.ClaimActions.MapUniqueJsonKey(JwtClaimTypes.Email, JwtClaimTypes.Email);
                   options.ClaimActions.MapUniqueJsonKey(JwtClaimTypes.Role, JwtClaimTypes.Role);
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

            services.AddScoped(typeof(QRCodeGenerator));
            services.AddScoped(typeof(DropboxClient), c => new DropboxClient(Configuration["dropboxApiKey"]));
            services.AddScoped<IFileUpload, FileUpload>();

            services.AddScoped<IEmailService>(c =>
            {
                var logger = c.GetService(typeof(ILogger<EmailService>)) as ILogger<EmailService>;
                string key = Configuration["sendgridKey"];
                string senderEmail = Configuration["senderEmail"];
                string senderName = Configuration["senderName"];
                return new EmailService(key, senderEmail, senderName, logger);
            });
            //AutoMapper
            AutoMapperConfiguration.Configure();
            //  MapperConfig.AutoMapperConfiguration.Configure();

            services.AddHttpContextAccessor();
            services.AddTransient<AuthenticatedHttpClientHandler>();

            services.AddScoped<HttpClient>();
            AddRefitServices(services);
            // services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".Awesomecare.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews()
                .AddSessionStateTempDataProvider();
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

            app.UseBaseRecordMiddleware();

            app.UseCookiePolicy();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     "{controller=Client}/{action=HomeCare}/{id?}").RequireAuthorization(apipolicyname);
            });
        }

        void AddRefitServices(IServiceCollection services)
        {
            string uri = Configuration["AwesomeCareBaseApi"];

            services.AddHttpClient("IdpClient", c =>
            {
                c.BaseAddress = new Uri(Configuration["idp_url"]);
            });

            services.AddHttpClient("companyservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICompanyService>(r))
         .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();



            services.AddHttpClient("companycontactservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICompanyContactService>(r))
          .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("baserecordservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IBaseRecordService>(r))
           .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientserviceparty", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientInvolvingParty>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientservicepartybase", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientInvolvingPartyBase>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientregulatorycontactservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRegulatoryContactService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientrotanameservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaNameService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientrotaservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            services.AddHttpClient("clientrotatypeservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaTypeService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientrotataskservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IRotaTaskService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("rotadayofweekservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IRotaDayofWeekService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientcaredetails", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientCareDetails>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffcommunication", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffCommunication>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("untowardsService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IUntowardsService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("shiftbookingservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IShiftBookingService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffworkteamservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffWorkTeamService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("medicationservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IMedicationService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffblacklistservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffBlackListService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("communicationService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICommunicationService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("incidentreportService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IIncidentReportService>(r))
           .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("investigationService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IInvestigationService>(r))
          .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
            
        }
    }
}
