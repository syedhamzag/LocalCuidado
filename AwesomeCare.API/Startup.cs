using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AwesomeCare.API.AppSettings;
using AwesomeCare.API.Middlewares;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AwesomeCareDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AwesomeCareConnectionString"));
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
            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AwesomeCare API", Version = "v1" });
               // c.ResolveConflictingActions(r => r.First());
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {

                    var settings = Configuration.GetSection("JwtBearerSettings").Get<JwtBearerSettings>();
                    options.Authority =settings.Authority;
                    options.RequireHttpsMetadata = settings.RequireHttpsMetadata;
                    options.SaveToken = settings.SaveToken;
                    options.Audience = settings.Audience;
                    
                });

            //  services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
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
                //  app.UseHsts();
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
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
           
        }
    }
}
