using System;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using AutoFacModuleScan.Abstractions;

namespace AutoFacModuleScan.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(b => b
                .ClearProviders()
                .SetMinimumLevel(LogLevel.Trace)
                .AddDebug()
                .AddConsole(c =>
                {
                    c.TimestampFormat = "HH:mm:ss.fff ";
                    c.IncludeScopes = true;
                }));

            var iocCont = ConfigureIoC(loggerFactory);

            var logger = iocCont.Resolve<ILogger<Program>>();

            logger.LogInformation("Starting.");

            using (var nc = iocCont.BeginLifetimeScope())
            {
                var svc = nc.Resolve<ITestDomSvc>();

                await svc.Run(args);
            }

            logger.LogInformation("Done.");
        }

        private static IContainer ConfigureIoC(ILoggerFactory loggerFactory)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(loggerFactory);
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            // Option 1 - Dynamically Scan 
            // builder.ScanForModulesInBaseFolder();

            // Option 2 (& 3) explicitly add modules (for 3 module is in Host Project)
            builder.RegisterModule<AutoFacModuleScan.Host.ConfigModule>();
            builder.RegisterModule<AutoFacModuleScan.Domain.ConfigModule>();

            return builder.Build();
        }
    }
}
