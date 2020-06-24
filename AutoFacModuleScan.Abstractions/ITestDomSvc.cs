using System;
using System.Threading.Tasks;

namespace AutoFacModuleScan.Abstractions
{
    public interface ITestDomSvc
    {
        Task Run(string[] args);
    }
}
