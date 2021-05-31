using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace AwesomeCare.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //NLog.LogManager.ThrowExceptions = true; // TODO Remove this when done trouble-shooting
            //NLog.Common.InternalLogger.LogLevel = NLog.LogLevel.Debug;
            //NLog.Common.InternalLogger.LogToConsole = true;
            //NLog.Common.InternalLogger.LogFile = "c:\temp\nlog-internal.txt";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Starting host...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
           // CreateWebHostBuilder(args).Build().Run();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               }).ConfigureLogging(logging =>
               {
                   logging.ClearProviders();
                   logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
               })
            .UseNLog();


        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //           WebHost.CreateDefaultBuilder(args)
        //               .UseStartup<Startup>();

    }
}
