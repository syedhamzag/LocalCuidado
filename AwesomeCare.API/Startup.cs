using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AwesomeCare.API.AppSettings;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.Model.Models;
using AwesomeCare.Services.Services;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AwesomeCare.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public static readonly ILoggerFactory DbLoggerFactory
    = LoggerFactory.Create(builder =>
    {
        builder
            .AddFilter((category, level) =>
            category == DbLoggerCategory.Database.Command.Name
            && level == LogLevel.Information).AddConsole().AddDebug();
    });
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; set; }
        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        const string apipolicyname = "api_bearerPolicy";
        public void ConfigureServices(IServiceCollection services)
        {
         
           
            services.AddControllers();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(apipolicyname, policy =>
                {
                   policy.AddAuthenticationSchemes("Bearer");
                   // policy.RequireClaim("scope", "awesomecareapi");
                    policy.RequireAuthenticatedUser();
                    
                });
            });
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
             .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, opt =>
             {
                 var settings = Configuration.GetSection("JwtBearerSettings").Get<JwtBearerSettings>();

                 opt.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                 opt.Authority = settings.Authority;
                 opt.ApiName = settings.Audience;
                 opt.JwtBearerEvents = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                 {
                     OnChallenge = ctx =>
                  {
                      ctx.Response.StatusCode = 401;
                      return Task.CompletedTask;
                  },
                     OnAuthenticationFailed = async ctx =>
                     {
                         ctx.Response.StatusCode = 401;
                         await ctx.Response.WriteAsync(ctx.Exception.Message);
                         // Task.CompletedTask;
                     },
                     OnForbidden = ctx =>
                     {
                         var kk = ctx;
                         return Task.CompletedTask;
                     },
                     OnMessageReceived = ctx =>
                     {
                         var kk = ctx;
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = ctx =>
                     {
                         var kk = ctx;
                         return Task.CompletedTask;
                     }
                 };
                 //opt.SaveToken = settings.SaveToken;
                 opt.SupportedTokens = SupportedTokens.Jwt;
             });
            //.AddJwtBearer("Bearer", options =>
            //{
            //    var settings = Configuration.GetSection("JwtBearerSettings").Get<JwtBearerSettings>();
            //    options.Authority = settings.Authority;
            //    options.RequireHttpsMetadata = true;// settings.RequireHttpsMetadata;
            //    options.SaveToken = settings.SaveToken;
            //    options.Audience = settings.Audience;

            //    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
            //    {
            //        OnChallenge = ctx =>
            //      {
            //          ctx.Response.StatusCode = 401;
            //          return Task.CompletedTask;
            //      },
            //        OnAuthenticationFailed = async ctx =>
            //        {
            //            ctx.Response.StatusCode = 401;
            //            await ctx.Response.WriteAsync(ctx.Exception.Message);
            //            // Task.CompletedTask;
            //        },
            //        OnForbidden = ctx =>
            //        {
            //            var kk = ctx;
            //            return Task.CompletedTask;
            //        },
            //        OnMessageReceived = ctx =>
            //        {
            //            var kk = ctx;
            //            return Task.CompletedTask;
            //        },
            //        OnTokenValidated = ctx =>
            //        {
            //            var kk = ctx;
            //            return Task.CompletedTask;
            //        }
            //    };
            //});
          
          //  var migrationAssembly = Assembly.Load("AwesomeCare.DataAccess").GetName().Name;
            services.AddDbContext<AwesomeCareDbContext>(options =>
            {
                options.UseLoggerFactory(DbLoggerFactory);
                options.UseSqlServer(Configuration.GetConnectionString("AwesomeCareConnectionString"));
                options.EnableSensitiveDataLogging(true);
            });
            services.AddLogging();
            services.Configure<JwtBearerSettings>(Configuration);
            #region AutoMapper
            //AutoMapper
            //  AutoMapperConfiguration.Configure();
            MapperConfig.AutoMapperConfiguration.Configure();
            #endregion
            #region Database
            services.AddScoped(typeof(DbContext));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion
            #region AspNetIdentity

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
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
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();
            #endregion

            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            //https://lurumad.github.io/swagger-ui-with-pkce-using-swashbuckle-asp-net-core { "openid", "openid" },{ "profile", "profile" },
            //https://medium.com/@taithienbo/configure-oauth2-implicit-flow-for-swagger-ui-b7d0181d4b0c
            //https://www.scottbrady91.com/Identity-Server/ASPNET-Core-Swagger-UI-Authorization-using-IdentityServer4
            services.AddSwaggerGen(c =>
            {
                if (CurrentEnvironment.IsDevelopment())
                {
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Flows = new OpenApiOAuthFlows()
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                Scopes = new Dictionary<string, string>() { { "awesomecareapi", "Awesome Care Api" } },
                                AuthorizationUrl = new Uri("https://localhost:44303/connect/authorize"),
                                TokenUrl = new Uri("https://localhost:44303/connect/token")
                            }
                        },
                        Description = "",
                        // In= ParameterLocation.Cookie,
                        Name = "Authorization",
                        Type = SecuritySchemeType.OAuth2,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        OpenIdConnectUrl = new Uri("https://localhost:44303/.well-known/openid-configuration")
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Flows = new OpenApiOAuthFlows
                                    {
                                         Implicit = new OpenApiOAuthFlow
                                            {
                                                Scopes = new Dictionary<string, string>() { { "awesomecareapi", "Awesome Care Api" } },
                                                AuthorizationUrl = new Uri("https://localhost:44303/connect/authorize"),
                                                TokenUrl = new Uri("https://localhost:44303/connect/token")
                                            }
                                    },
                                   Description = "",
                                   Name = "Authorization",
                                   OpenIdConnectUrl = new Uri("https://localhost:44303/.well-known/openid-configuration"),
                                   Scheme="Bearer",
                                   Type = SecuritySchemeType.OAuth2,
                                   Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,Id = "oauth2"}
                                },new List<string>(){ "awesomecareapi", "awesomecareapi" }
                            }

                    });
                }


                //  c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AwesomeCare API", Version = "v1" });
                // c.ResolveConflictingActions(r => r.First());
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

            services.AddScoped<IFileUpload, FileUpload>();
            //  services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CurrentEnvironment = env;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }

            //  app.UseMiddleware<RemoveResponseHeaderMiddleware>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                if (env.IsStaging())
                {
                    c.SwaggerEndpoint("/awesomecareapi/swagger/v1/swagger.json", "AwesomeCare API V1");
                }
                else
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AwesomeCare API V1");
                }
                c.RoutePrefix = string.Empty;
                if (env.IsDevelopment())
                {
                    c.OAuthClientId("9e0024c1b7e7479d8fae56848499f35a");
                    c.OAuthClientSecret("80ElH7wqOmuPUcA+5KJFbvSLOQDT6aN6OcQwXnpvFCw=");
                    c.OAuthScopeSeparator(" ");
                    c.OAuthUsePkce();
                }

            });

            app.UseHttpsRedirection();

            app.UseRouting();
            //  app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                //if (env.IsDevelopment())
                //    endpoints.MapControllers();//.RequireAuthorization();
                //else
                endpoints.MapControllers().RequireAuthorization(apipolicyname);
            });

        }
    }
}
