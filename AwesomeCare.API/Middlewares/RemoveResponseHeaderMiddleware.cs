using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Middlewares
{
    public class RemoveResponseHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        public RemoveResponseHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try

            {
                //context.Response.OnStarting(  state =>
                //{
                //    var ctx = (HttpContext)state;
                //    if (ctx.Response.Headers.ContainsKey("x-powered-by") || ctx.Response.Headers.ContainsKey("X-Powered-By"))
                //    {
                //        ctx.Response.Headers.Remove("x-powered-by");
                //        ctx.Response.Headers.Remove("X-Powered-By");
                //    }

                //    return Task.FromResult(0);
                //},context);
               
               // context.Response.Headers.Add("me", "olamide");
                await _next.Invoke(context);
                if (context.Response.Headers.ContainsKey("x-powered-by") || context.Response.Headers.ContainsKey("X-Powered-By"))
                {
                    context.Response.Headers.Remove("x-powered-by");
                    context.Response.Headers.Remove("X-Powered-By");
                }

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
