using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;

namespace AutoFacModuleScan.Host
{
    public static class AutofacRegistraionExt
    {
        public static ContainerBuilder ScanForModulesInBaseFolder(this ContainerBuilder builder)
        {
            var assemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory)
                .Select(LoadAsm)
                .Where(x => x != null)
                .ToArray();

            builder.RegisterAssemblyModules(assemblies);

            return builder;
        }

        private static Assembly LoadAsm(string fileName)
        {
            try
            {
                return Assembly.LoadFrom(fileName);
            }
            catch
            {
                return null;
            }
        }

    }
}
