using System;
using Autofac;
using AutoFacModuleScan.Abstractions;

namespace AutoFacModuleScan.Domain
{
    public class ConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestDomSvc>().As<ITestDomSvc>();
        }
    }
}
