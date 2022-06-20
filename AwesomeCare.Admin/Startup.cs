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
using AwesomeCare.Admin.Services.HealthAndLiving;
using AwesomeCare.Admin.Services.SpecialHealthAndMedication;
using AwesomeCare.Admin.Services.Balance;
using AwesomeCare.Admin.Services.PhysicalAbility;
using AwesomeCare.Admin.Services.SpecialHealthCondition;
using AwesomeCare.Admin.Services.HistoryOfFall;
using AwesomeCare.Admin.Services.CarePlanNutrition;
using AwesomeCare.Admin.Services.PersonalHygiene;
using AwesomeCare.Admin.Services.InfectionControl;
using AwesomeCare.Admin.Services.OfficeLocation;
using AwesomeCare.Admin.Services.ManagingTasks;
using AwesomeCare.Admin.Services.Dashboard;
using AwesomeCare.Admin.Services.InterestAndObjective;
using AwesomeCare.Admin.Services.Pets;
using AwesomeCare.Admin.Services.TaskBoard;
using AwesomeCare.Admin.Services.HospitalEntry;
using AwesomeCare.Admin.Services.HospitalExit;
using AwesomeCare.Admin.Services.DutyOnCall;
using AwesomeCare.Admin.Services.TrackingConcernNote;
using AwesomeCare.Admin.Services.StaffCompetenceTest;
using AwesomeCare.Admin.Services.StaffHealth;
using AwesomeCare.Admin.Services.StaffInterview;
using AwesomeCare.Admin.Services.StaffShadowing;
using AwesomeCare.Admin.Services.PerformanceIndicator;
using AwesomeCare.Admin.Services.ClientDailyTask;
using AwesomeCare.Admin.Services.StaffHoliday;
using AwesomeCare.Admin.Services.StaffTeamLead;
using AwesomeCare.Admin.Services.BestInterestAssessment;
using AwesomeCare.Admin.Services.FilesAndRecord;
using AwesomeCare.Admin.Services.SalaryAllowance;
using AwesomeCare.Admin.Services.SalaryDeduction;
using AwesomeCare.Admin.Services.ClientCareObj;
using AwesomeCare.Admin.Services.Payroll;
using AwesomeCare.Admin.Services.Chat;
using static AwesomeCare.Admin.Controllers.ChatController;

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

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(apipolicyname, policy =>
            //    {
            //        policy.AddAuthenticationSchemes("OpenIdConnect", "Identity.Application");
            //        policy.RequireAuthenticatedUser();

            //    });
            //});
            //services.AddAuthentication("OpenIdConnect")
            //    .AddCookie("Identity.Application", options =>
            //    {
            //        options.Events = new CookieAuthenticationEvents
            //        {
            //            OnRedirectToAccessDenied = ctx =>
            //             {
            //                 var tt = ctx;
            //                 logger.LogInformation($"OnRedirectToAccessDenied");
            //                 return Task.CompletedTask;
            //             },
            //            OnRedirectToLogin = ctx =>
            //             {
            //                 var tt = ctx;
            //                 logger.LogInformation($"OnRedirectToLogin");
            //                 return Task.CompletedTask;
            //             },
            //            OnRedirectToLogout = ctx =>
            //             {
            //                 var tt = ctx;
            //                 logger.LogInformation($"OnRedirectToLogout");
            //                 return Task.CompletedTask;
            //             },
            //            OnRedirectToReturnUrl = ctx =>
            //             {
            //                 var tt = ctx;
            //                 logger.LogInformation($"OnRedirectToReturnUrl");
            //                 return Task.CompletedTask;
            //             },
            //            OnSignedIn = ctx =>
            //             {
            //                 var tt = ctx;
            //                 logger.LogInformation($"OnSignedIn");
            //                 return Task.CompletedTask;
            //             },
            //            OnSigningOut = ctx =>
            //             {
            //                 var tt = ctx;
            //                 logger.LogInformation($"OnSigningOut");
            //                 return Task.CompletedTask;
            //             },
            //            OnValidatePrincipal = ctx =>
            //             {
            //                 var tt = ctx;
            //                 logger.LogInformation($"OnValidatePrincipal");
            //                 return Task.CompletedTask;
            //             }
            //        };
            //    })
            //   .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            //   {
                 
            //       var settings = Configuration.GetSection("IDPClientSettings").Get<IDPClientSettings>();
            //       options.SignInScheme = "Identity.Application";//  CookieAuthenticationDefaults.AuthenticationScheme;
            //       options.Authority = Configuration["idp_url"].ToString();
            //       options.ClientId = settings.ClientId;
            //       options.ResponseType = "code";
            //       options.SignedOutRedirectUri = Configuration["app_url"];// + "/signout-callback-oidc";
            //      // options.SignedOutCallbackPath = new PathString("/signout-callback-oidc");
            //       foreach (string scope in settings.Scopes)
            //       {
            //           options.Scope.Add(scope);
            //       }
            //       options.SaveTokens = true;
            //       options.ClientSecret = settings.ClientSecret;
            //       options.GetClaimsFromUserInfoEndpoint = true;
            //       //Remove Unnecessary claims
            //       options.ClaimActions.DeleteClaim("s_hash");
            //       options.ClaimActions.DeleteClaim("auth_time");
            //       options.ClaimActions.DeleteClaim("sid");
            //       options.ClaimActions.DeleteClaim("idp");

            //       //Mapp Additional Claims as Configured in IProfileService in Identity Server Project
            //       // options.ClaimActions.MapUniqueJsonKey("hasStaffInfo", "hasStaffInfo");
            //       options.ClaimActions.MapUniqueJsonKey(JwtClaimTypes.Email, JwtClaimTypes.Email);
            //       options.ClaimActions.MapUniqueJsonKey(JwtClaimTypes.Role, JwtClaimTypes.Role);
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           NameClaimType = JwtClaimTypes.Name,
            //           RoleClaimType = JwtClaimTypes.Role
            //       };
            //       options.Events = new OpenIdConnectEvents
            //       {
            //           OnAccessDenied = ctx =>
            //           {
            //               var tt = ctx;
            //               logger.LogInformation($"OnAccessDenied");
            //               return Task.CompletedTask;
            //           },
            //           OnAuthenticationFailed = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnAuthenticationFailed {ctx.Exception?.Message}");
            //                return Task.CompletedTask;
            //            },
            //           OnAuthorizationCodeReceived = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnAuthorizationCodeReceived");
            //                return Task.CompletedTask;
            //            },
            //           OnMessageReceived = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnMessageReceived");
            //                return Task.CompletedTask;
            //            },
            //           OnRedirectToIdentityProvider = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnRedirectToIdentityProvider");
            //                return Task.CompletedTask;
            //            },
            //           OnRedirectToIdentityProviderForSignOut = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnRedirectToIdentityProviderForSignOut");
            //                return Task.CompletedTask;
            //            },
            //           OnRemoteFailure = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation($"OnRemoteFailure {ctx.Failure?.Message}");
            //                return Task.CompletedTask;
            //            },
            //           OnRemoteSignOut = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnRemoteSignOut");
            //                return Task.CompletedTask;
            //            },
            //           OnSignedOutCallbackRedirect = ctx =>
            //            {
            //                var tt = ctx;
                           
            //                return Task.CompletedTask;
            //            },
            //           OnTicketReceived = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnTicketReceived");
            //                return Task.CompletedTask;
            //            },
            //           OnTokenResponseReceived = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnTokenResponseReceived");
            //                return Task.CompletedTask;
            //            },
            //           OnTokenValidated = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnTokenValidated");
            //                return Task.CompletedTask;
            //            },
            //           OnUserInformationReceived = ctx =>
            //            {
            //                var tt = ctx;
            //                logger.LogInformation("OnUserInformationReceived");
            //                return Task.CompletedTask;
            //            }
            //       };

            //   });

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
            services.AddSignalR();
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
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Dashboard}");
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
;



            services.AddHttpClient("companycontactservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICompanyContactService>(r))
 ;

            services.AddHttpClient("baserecordservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IBaseRecordService>(r))
  ;

            services.AddHttpClient("clientservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientService>(r))
   ;

            services.AddHttpClient("clientserviceparty", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientInvolvingParty>(r))
   ;

            services.AddHttpClient("clientservicepartybase", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientInvolvingPartyBase>(r))
   ;

            services.AddHttpClient("clientregulatorycontactservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRegulatoryContactService>(r))
   ;

            services.AddHttpClient("clientrotanameservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaNameService>(r))
   ;

            services.AddHttpClient("clientrotaservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaService>(r))
   ;


            services.AddHttpClient("clientrotatypeservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientRotaTypeService>(r))
   ;

            services.AddHttpClient("clientrotataskservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IRotaTaskService>(r))
   ;

            services.AddHttpClient("rotadayofweekservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IRotaDayofWeekService>(r))
   ;

            services.AddHttpClient("clientcaredetails", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientCareDetails>(r))
   ;

            services.AddHttpClient("staffservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffService>(r))
   ;

            services.AddHttpClient("staffcommunication", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffCommunication>(r))
   ;

            services.AddHttpClient("untowardsService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IUntowardsService>(r))
   ;

            services.AddHttpClient("shiftbookingservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IShiftBookingService>(r))
   ;

            services.AddHttpClient("staffworkteamservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffWorkTeamService>(r))
   ;

            services.AddHttpClient("medicationservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IMedicationService>(r))
   ;

            services.AddHttpClient("complainservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IComplainService>(r))
   ;

            services.AddHttpClient("nutritionservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<INutritionService>(r))
   ;

            services.AddHttpClient("staffblacklistservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffBlackListService>(r))
   ;

            services.AddHttpClient("communicationService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICommunicationService>(r))
   ;

            services.AddHttpClient("incidentreportService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IIncidentReportService>(r))
  ;

            services.AddHttpClient("investigationService", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IInvestigationService>(r))
 ;

            services.AddHttpClient("userservice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IUserService>(r))
;

            services.AddHttpClient("clientlogservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientLogAuditService>(r))
   ;

            services.AddHttpClient("clientmedservie", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientMedAuditService>(r))
   ;

            services.AddHttpClient("clientvoice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientVoiceService>(r))
   ;

            services.AddHttpClient("clientmgtvisit", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientMgtVisitService>(r))
   ;

            services.AddHttpClient("clientprogram", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientProgramService>(r))
   ;

            services.AddHttpClient("clientservicewatch", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientServiceWatchService>(r))
   ;

            services.AddHttpClient("staffspotcheck", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffSpotCheckService>(r))
   ;

            services.AddHttpClient("staffadlobs", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffAdlObsService>(r))
   ;

            services.AddHttpClient("staffmedcomp", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffMedCompService>(r))
   ;

            services.AddHttpClient("staffkeyworkervoice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffKeyWorkerVoiceService>(r))
   ;

            services.AddHttpClient("staffsurvey", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffSurveyService>(r))
   ;

            services.AddHttpClient("staffonetoone", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffOneToOneService>(r))
   ;

            services.AddHttpClient("staffsupervisionappraisal", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffSupervisionAppraisalService>(r))
   ;

            services.AddHttpClient("staffreference", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffReferenceService>(r))
   ;

            services.AddHttpClient("enotice", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IEnoticeService>(r))
   ;

            services.AddHttpClient("resources", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IResourcesService>(r))
   ;

            services.AddHttpClient("incomingmeds", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IIncomingMedsService>(r))
   ;

            services.AddHttpClient("whisttleblower", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IWhisttleBlowerService>(r))
   ;

            services.AddHttpClient("clientbloodcoagulationrecord", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBloodCoagulationRecordService>(r))
   ;

            services.AddHttpClient("clientbloodpressure", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBloodPressureService>(r))
   ;

            services.AddHttpClient("clientbmichart", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBMIChartService>(r))
   ;

            services.AddHttpClient("clientbodytemp", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBodyTempService>(r))
   ;

            services.AddHttpClient("clientbowelmovement", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientBowelMovementService>(r))
   ;

            services.AddHttpClient("clienteyehealth", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientEyeHealthMonitoringService>(r))
   ;

            services.AddHttpClient("clientfoodintake", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientFoodIntakeService>(r))
   ;

            services.AddHttpClient("clientheartrate", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientHeartRateService>(r))
   ;

            services.AddHttpClient("clientoxygenlvl", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientOxygenLvlService>(r))
   ;

            services.AddHttpClient("clientpainchart", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientPainChartService>(r))
   ;

            services.AddHttpClient("clientpulserate", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientPulseRateService>(r))
   ;

            services.AddHttpClient("clientseizure", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientSeizureService>(r))
   ;

            services.AddHttpClient("clientwoundcare", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientWoundCareService>(r))
   ;

            services.AddHttpClient("capacity", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICapacityService>(r))
   ;

            services.AddHttpClient("consentcare", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IConsentCareService>(r))
   ;

            services.AddHttpClient("consentdata", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IConsentDataService>(r))
   ;

            services.AddHttpClient("consentlandline", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IConsentLandLineService>(r))
   ;

            services.AddHttpClient("equipment", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IEquipmentService>(r))
   ;

            services.AddHttpClient("keyindicators", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IKeyIndicatorsService>(r))
   ;

            services.AddHttpClient("personal", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPersonalService>(r))
   ;

            services.AddHttpClient("personcentred", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPersonCentredService>(r))
   ;

            services.AddHttpClient("review", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IReviewService>(r))
   ;

            services.AddHttpClient("personaldetail", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPersonalDetailService>(r))
   ;

            services.AddHttpClient("healthliving", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IHealthAndLivingService>(r))
   ;

            services.AddHttpClient("specialhealthandmedication", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ISpecialHealthAndMedicationService>(r))
   ;

            services.AddHttpClient("balance", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IBalanceService>(r))
   ;

            services.AddHttpClient("physicalability", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPhysicalAbilityService>(r))
   ;

            services.AddHttpClient("specialhealthcondition", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ISpecialHealthConditionService>(r))
   ;

            services.AddHttpClient("historyoffall", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IHistoryOfFallService>(r))
   ;

            services.AddHttpClient("careplannutrition", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ICarePlanNutritionService>(r))
   ;

            services.AddHttpClient("careplanhygiene", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPersonalHygieneService>(r))
   ;

            services.AddHttpClient("infectioncontrol", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IInfectionControlService>(r))
   ;

            services.AddHttpClient("officelocation", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IOfficeLocationService>(r))
   ;

            services.AddHttpClient("managingtask", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IManagingTasksService>(r))
   ;

            services.AddHttpClient("dashboard", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IDashboardService>(r))
   ;

            services.AddHttpClient("interestandobjective", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IInterestAndObjectiveService>(r))
   ;

            services.AddHttpClient("pets", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPetsService>(r))
   ;

            services.AddHttpClient("taskboard", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ITaskBoardService>(r))
   ;

            services.AddHttpClient("hospitalentry", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IHospitalEntryService>(r))
   ;

            services.AddHttpClient("hospitalexit", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IHospitalExitServices>(r))
   ;

            services.AddHttpClient("staffpersonalitytest", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffPersonalityTest>(r))
   ;

            services.AddHttpClient("homeriskassessment", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IHomeRiskAssessmentService>(r))
   ;

            services.AddHttpClient("staffinfectioncontrol", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffInfectionControlService>(r))
   ;

            services.AddHttpClient("dutyoncallt", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IDutyOnCallService>(r))
   ;

            services.AddHttpClient("trackingconcernnote", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ITrackingConcernNote>(r))
   ;

            services.AddHttpClient("competencetest", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffCompetenceTestService>(r))
   ;

            services.AddHttpClient("staffhealth", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffHealthService>(r))
   ;

            services.AddHttpClient("staffinterview", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffInterviewService>(r))
   ;

            services.AddHttpClient("staffshadowing", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffShadowingService>(r))
   ;

            services.AddHttpClient("performanceindicator", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPerformanceIndicatorService>(r))
   ;

            services.AddHttpClient("clientdailytask", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientDailyTaskService>(r))
   ;

            services.AddHttpClient("staffholiday", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffHolidayService>(r))
   ;

            services.AddHttpClient("setupstaffholiday", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ISetupStaffHolidayService>(r))
   ;

            services.AddHttpClient("staffteamlead", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffTeamLeadService>(r))
   ;

            services.AddHttpClient("bestinterestassessment", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IBestInterestAssessmentService>(r))
   ;

            services.AddHttpClient("stafftrainingmatrix", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffTrainingMatrixService>(r))
   ;

            services.AddHttpClient("FilesAndRecord", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IFilesAndRecordService>(r))
   ;

            services.AddHttpClient("SalaryAllowance", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ISalaryAllowanceService>(r))
   ;

            services.AddHttpClient("SalaryDeduction", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<ISalaryDeductionService>(r))
   ;

            services.AddHttpClient("StaffTax", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IStaffTaxService>(r))
   ;

            services.AddHttpClient("chat", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IChatService>(r))
   ;

            services.AddHttpClient("payroll", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IPayrollService>(r))
   ;

            services.AddHttpClient("careobj", c =>
            {
                c.BaseAddress = new Uri(uri);
            }).AddTypedClient(r => RestService.For<IClientCareObjService>(r))
   ;
        }
    }
}
