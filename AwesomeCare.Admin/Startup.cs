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
using AwesomeCare.Admin.Services.ComplainRegister;
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
using AwesomeCare.Admin.Services.User;
using AwesomeCare.Admin.Services.Nutrition;
using AwesomeCare.Admin.Services.ClientLogAudit;
using AwesomeCare.Admin.Services.ClientMedAudit;
using AwesomeCare.Admin.Services.ClientVoice;
using AwesomeCare.Admin.Services.ClientMgtVisit;
using AwesomeCare.Admin.Services.ClientProgram;
using AwesomeCare.Admin.Services.ClientServiceWatch;
using AwesomeCare.Admin.Services.StaffSpotCheck;
using AwesomeCare.Admin.Services.StaffAdlObs;
using AwesomeCare.Admin.Services.StaffMedComp;
using AwesomeCare.Admin.Services.StaffKeyWorkerVoice;
using AwesomeCare.Admin.Services.StaffSurvey;
using AwesomeCare.Admin.Services.StaffOneToOne;
using AwesomeCare.Admin.Services.StaffSupervisionAppraisal;
using AwesomeCare.Admin.Services.StaffReference;
using AwesomeCare.Admin.Services.Enotice;
using AwesomeCare.Admin.Services.Resources;
using AwesomeCare.Admin.Services.IncomingMeds;
using AwesomeCare.Admin.Services.WhisttleBlower;
using AwesomeCare.Admin.Services.ClientBloodCoagulationRecord;
using AwesomeCare.Admin.Services.ClientBloodPressure;
using AwesomeCare.Admin.Services.ClientBMIChart;
using AwesomeCare.Admin.Services.ClientBodyTemp;
using AwesomeCare.Admin.Services.ClientBowelMovement;
using AwesomeCare.Admin.Services.ClientEyeHealthMonitoring;
using AwesomeCare.Admin.Services.ClientFoodIntake;
using AwesomeCare.Admin.Services.ClientHeartRate;
using AwesomeCare.Admin.Services.ClientOxygenLvl;
using AwesomeCare.Admin.Services.ClientPainChart;
using AwesomeCare.Admin.Services.ClientPulseRate;
using AwesomeCare.Admin.Services.ClientSeizure;
using AwesomeCare.Admin.Services.ClientWoundCare;
using AwesomeCare.Admin.Services.Capacity;
using AwesomeCare.Admin.Services.ConsentCare;
using AwesomeCare.Admin.Services.ConsentData;
using AwesomeCare.Admin.Services.ConsentLandLine;
using AwesomeCare.Admin.Services.Equipment;
using AwesomeCare.Admin.Services.KeyIndicators;
using AwesomeCare.Admin.Services.Personal;
using AwesomeCare.Admin.Services.PersonCentred;
using AwesomeCare.Admin.Services.Review;
using AwesomeCare.Admin.Services.PersonalDetail;

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
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
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

            services.AddHttpClient("complainservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IComplainService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("nutritionservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<INutritionService>(r))
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

            services.AddHttpClient("userservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IUserService>(r))
         .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientlogservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientLogAuditService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientmedservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientMedAuditService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientvoice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientVoiceService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientmgtvisit", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientMgtVisitService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientprogram", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientProgramService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientservicewatch", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientServiceWatchService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffspotcheck", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffSpotCheckService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffadlobs", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffAdlObsService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffmedcomp", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffMedCompService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffkeyworkervoice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffKeyWorkerVoiceService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffsurvey", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffSurveyService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffonetoone", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffOneToOneService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffsupervisionappraisal", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffSupervisionAppraisalService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("staffreference", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffReferenceService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("enotice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IEnoticeService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("resources", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IResourcesService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("incomingmeds", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IIncomingMedsService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("whisttleblower", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IWhisttleBlowerService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientbloodcoagulationrecord", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBloodCoagulationRecordService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientbloodpressure", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBloodPressureService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientbmichart", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBMIChartService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientbodytemp", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBodyTempService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientbowelmovement", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBowelMovementService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clienteyehealth", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientEyeHealthMonitoringService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientfoodintake", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientFoodIntakeService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientheartrate", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientHeartRateService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientoxygenlvl", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientOxygenLvlService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientpainchart", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientPainChartService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientpulserate", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientPulseRateService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientseizure", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientSeizureService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("clientwoundcare", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientWoundCareService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("capacity", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICapacityService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("consentcare", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IConsentCareService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("consentdata", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IConsentDataService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("consentlandline", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IConsentLandLineService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("equipment", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IEquipmentService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("keyindicators", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IKeyIndicatorsService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("personal", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPersonalService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("personcentred", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPersonCentredService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("review", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IReviewService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            services.AddHttpClient("personaldetail", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPersonalDetailService>(r))
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
        }
    }
}
