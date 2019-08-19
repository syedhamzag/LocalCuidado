using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AwesomeCare.API.AutoMapperConfig;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AwesomeCare.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<AwesomeCareDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AwesomeCareConnectionString"));
            });
            services.AddLogging();

            #region AutoMapper
            //AutoMapper
            AutoMapperConfiguration.Configure();
            #endregion
            #region Database
            services.AddScoped(typeof(IDbContext), typeof(AwesomeCareDbContext));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion
            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AwesomeCare API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

          //  services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
            //app.UseMvc(routeBuilder=> {
            //    routeBuilder.EnableDependencyInjection();
            //    routeBuilder.Expand().Select().OrderBy().Filter().MaxTop(1000);
            //});
        }
    }
}
