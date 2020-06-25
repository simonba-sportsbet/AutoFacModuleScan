using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;

namespace AutoFacModuleScan.Host
{
    public static class AutofacRegistraionExt
    {
        public static readonly string[] DefaultAssemblyFileExtensions = new[] { "dll", "exe" };

        public static ContainerBuilder ScanForModulesInBaseFolder(
            this ContainerBuilder builder, 
            string[] assemblyFileExtensions = null, 
            Func<string, bool> assemblyFileFilter = null)
        {
            var filenameFilter = assemblyFileFilter ??
                ((string x) => (assemblyFileExtensions ?? DefaultAssemblyFileExtensions).Any(fx => x.EndsWith(fx, StringComparison.OrdinalIgnoreCase)));

            var assemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory)
                .Where(filenameFilter)
                .Select(LoadAssembly)
                .Where(x => x != null)
                .ToArray();

            builder.RegisterAssemblyModules(assemblies);

            return builder;
        }

        private static Assembly LoadAssembly(string fileName)
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
