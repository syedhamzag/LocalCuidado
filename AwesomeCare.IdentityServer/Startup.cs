// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityExpress.Identity;
using IdentityExpress.Manager.Api;
using IdentityServer4;
using IdentityServer4.Configuration;
using AwesomeCare.IdentityServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using AwesomeCare.Model.Models;
using AwesomeCare.DataAccess.Database;
using IdentityServer4.Services;
using AwesomeCare.IdentityServer.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Serilog;
using AwesomeCare.Services.Services;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;
using IdentityExpress.Manager.BusinessLogic.Interfaces.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Mandrill;

namespace AwesomeCare.IdentityServer
{
    public class Startup
    {
        private ILogger<Startup> logger;
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public static readonly ILoggerFactory DbLoggerFactory
   = LoggerFactory.Create(builder =>
   {
       builder
.AddFilter((category, level) =>
category == DbLoggerCategory.Database.Command.Name
&& level == LogLevel.Information).AddConsole().AddDebug();
   });


        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            //// configures IIS out-of-proc settings (see https://github.com/aspnet/AspNetCore/issues/14882)
            //services.Configure<IISOptions>(iis =>
            //{
            //    iis.AuthenticationDisplayName = "Windows";
            //    iis.AutomaticAuthentication = false;
            //});

            //// configures IIS in-proc settings
            //services.Configure<IISServerOptions>(iis =>
            //{
            //    iis.AuthenticationDisplayName = "Windows";
            //    iis.AutomaticAuthentication = false;
            //});

            //  services.AddScoped<UserManager<IdentityUser>>();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("SuperAdminPolicy", new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .RequireClaim("role", "Staff,SuperAdmin")
            //        .Build());
            //});

            services.AddDbContext<AwesomeCareDbContext>(options =>
            {
                options.UseLoggerFactory(DbLoggerFactory);
                options.UseSqlServer(Configuration.GetConnectionString("AwesomeCareConnectionString"));
                options.EnableSensitiveDataLogging(true);
            });

            //  services.AddTransient<IUserTwoFactorTokenProvider<ApplicationUser>, DataProtectorTokenProvider<ApplicationUser>>();
            //  services.AddTransient<DataProtectorTokenProvider<ApplicationUser>>();

            var identityUser = services.AddIdentity<ApplicationUser, IdentityRole>(options =>
             {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.User.RequireUniqueEmail = true;
                 options.SignIn.RequireConfirmedAccount = true;
                 //  options.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(DataProtectorTokenProvider<ApplicationUser>)));

             })
                   .AddEntityFrameworkStores<AwesomeCareDbContext>()
                .AddDefaultTokenProviders();


            var connectionString = Configuration.GetConnectionString("AwesomeCareConnectionString");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var builder = services.AddIdentityServer(options =>
                {
                   
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    //options.Authentication = new AuthenticationOptions
                    //{
                    //    CookieLifetime = TimeSpan.FromMinutes(1),
                    //    CookieSlidingExpiration = false
                    //};
                    options.UserInteraction = new UserInteractionOptions
                    {
                        LogoutUrl = "/Account/Logout",
                        LoginUrl = "/Account/Login",
                        LoginReturnUrlParameter = "returnUrl"
                    };
                })
                .AddAspNetIdentity<ApplicationUser>()
                // this adds the config data from DB (clients, resources, CORS)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(connectionString,
                                                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(connectionString,
                                                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    // options.TokenCleanupInterval = 15; // interval in seconds. 15 seconds useful for debugging
                });

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
            services.AddOidcStateDataFormatterCache();
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //   .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            //   options =>
            //   {

            //   });

            //This is preventing Password Reset Toke, Phone Number Confirmation Token and other tokens from working
            //if (Environment.IsDevelopment())
            //    services.UseAdminUI();

            services.AddScoped<IProfileService, ProfileService>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IEmailService>(c =>
            {
                var logger = c.GetService(typeof(ILogger<EmailService>)) as ILogger<EmailService>;
                string key = Configuration["sendgridKey"];
                string senderEmail = Configuration["senderEmail"];
                string senderName = Configuration["senderName"];
                return new EmailService(key, senderEmail, senderName, logger);
            });
            services.AddScoped<IMandrillApi>(c => { return new MandrillApi(Configuration["MailChimpSettings:Key"]); });

            services.AddScoped<IMailChimpService, MailChimpService>();

            services.AddLogging(c =>
            {
                c.AddConsole();
                c.AddDebug();
                c.AddAzureWebAppDiagnostics();
            });
        }

        private X509Certificate2 GetSigningCertificate()
        {
            X509Certificate2 cert = null;
            string certThumbPrint = Configuration["CertThumbPrint"];
            using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificateCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint,
                    certThumbPrint, true);

                if (certificateCollection.Count > 0) 
                    cert = certificateCollection[0];
            }
            return cert;
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            this.logger = logger;
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            //if (Environment.IsDevelopment())
            //    app.UseAdminUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                     "{controller=Client}/{action=Index}/{id?}");

            });
        }
    }
}
