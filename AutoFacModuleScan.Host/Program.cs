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
                .AddDebug()
                .AddConsole());

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

            builder.ScanForModulesInBaseFolder();

            return builder.Build();
        }
    }
}
