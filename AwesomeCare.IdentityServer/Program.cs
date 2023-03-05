// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using NLog.Web;

namespace AwesomeCare.IdentityServer
{
    public class Program
    {

        public static int Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //    .MinimumLevel.Override("System", LogEventLevel.Warning)
            //    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            // uncomment to write to Azure diagnostics stream
            //.WriteTo.File(
            //    @"D:\home\LogFiles\Application\identityserver.txt",
            //    fileSizeLimitBytes: 1_000_000,
            //    rollOnFileSizeLimit: true,
            //    shared: true,
            //    flushToDiskInterval: TimeSpan.FromSeconds(1))
            //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
            //.CreateLogger();

            try
            {
                logger.Debug("Starting host...");

                var host = CreateHostBuilder(args).Build();


                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                // Log.CloseAndFlush();
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseSerilog();
                    webBuilder.ConfigureKestrel(options => options.AllowSynchronousIO = true);
                })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
             .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}
