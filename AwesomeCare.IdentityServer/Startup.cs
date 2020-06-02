﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
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

namespace AwesomeCare.IdentityServer
{
    public class Startup
    {
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

            services.AddDbContext<AwesomeCareDbContext>(options =>
            {
                options.UseLoggerFactory(DbLoggerFactory);
                options.UseSqlServer(Configuration.GetConnectionString("AwesomeCareConnectionString"));
                options.EnableSensitiveDataLogging(true);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;


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

            
            //this must be the same thing configured on all clients
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;//
                options.DefaultChallengeScheme =  OpenIdConnectDefaults.AuthenticationScheme;

            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    //  options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.Cookie.Name = ".AwesomeCare.Cookie";
                });

            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        // register your IdentityServer with Google at https://console.developers.google.com
            //        // enable the Google+ API
            //        // set the redirect URI to http://localhost:5000/signin-google
            //        options.ClientId = "copy client ID from Google here";
            //        options.ClientSecret = "copy client secret from Google here";
            //    });

            if (Environment.IsDevelopment())
                services.UseAdminUI();

            services.AddScoped<IProfileService, ProfileService>();
            services.AddSingleton<IEmailSender, EmailSender>();
            // services.AddScoped<IdentityExpressDbContext, SqliteIdentityDbContext>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            //if (Environment.IsDevelopment())
            //    app.UseAdminUI();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                     "{controller=Client}/{action=Index}/{id?}");
               
            });
        }
    }
}
