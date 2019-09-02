using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace AwesomeCare.Admin.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BaseRecordMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly IBaseRecordService _baseRecordService;
        private const string cacheKey = "baserecord_key";
        public BaseRecordMiddleware(RequestDelegate next, IMemoryCache cache, IBaseRecordService baseRecordService)
        {
            _next = next;
            _cache = cache;
            _baseRecordService = baseRecordService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(!_cache.TryGetValue(cacheKey, out List<GetBaseRecordWithItems> baseRecords))
            {
                var records = await _baseRecordService.GetBaseRecordsWithItems();

                //Save BaseRecords to Cache
                _cache.Set(cacheKey, records);
            }
            
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BaseRecordMiddlewareExtensions
    {
        public static IApplicationBuilder UseBaseRecordMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BaseRecordMiddleware>();
        }
    }
}
