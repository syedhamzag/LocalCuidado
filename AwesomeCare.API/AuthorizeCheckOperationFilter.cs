using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {


        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = true;
            //context.ControllerActionDescriptor
            //       .GetControllerAndActionAttributes(true)
            //       .OfType<AuthorizeAttribute>()
            //       .Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
                
                //operation.Security = new List<OpenApiSecurityRequirement>
                //{
                //   {
                //        new OpenApiSecurityScheme
                //        {
                //           Description = "",
                //           Flows = new OpenApiOAuthFlows
                //           {
                //               AuthorizationCode = new OpenApiOAuthFlow
                //                {
                //                    Scopes = new Dictionary<string, string>() { { "openid", "openid" }, { "profile", "profile" }, { "awesomecareapi", "awesomecareapi" } },
                //                    AuthorizationUrl = new Uri("https://localhost:44303/connect/authorize"),
                //                    TokenUrl = new Uri("https://localhost:44303/connect/token")
                //                }
                //           },
                //           Name = "Authorization",
                //           OpenIdConnectUrl= new System.Uri(""),
                //           Scheme = "Bearer",
                //           Type = SecuritySchemeType.OAuth2

                //         },
                //         new List<string>(){"kkk"}
                //     }
                //};
            };
        }
    }
}

