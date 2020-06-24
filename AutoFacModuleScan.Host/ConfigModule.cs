using System;
using Autofac;
using AutoFacModuleScan.Abstractions;

namespace AutoFacModuleScan.Host
{
    public class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TimeProvider>().As<ITimeProvider>();
        }
    }
}
