using System;
using System.Threading.Tasks;
using AutoFacModuleScan.Abstractions;
using Microsoft.Extensions.Logging;

namespace AutoFacModuleScan.Domain
{
    public class TestDomSvc : ITestDomSvc, IDisposable //, IAsyncDisposable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly ILogger _logger;

        public TestDomSvc(
            ITimeProvider timeProvider,
            ILogger<TestDomSvc> logger
            )
        {
            _timeProvider = timeProvider;
            _logger = logger;
        }

        public void Dispose()
        {
            _logger.LogDebug("Cleaning up...");
        }

        //public ValueTask DisposeAsync()
        //{
        //    _logger.LogDebug("Cleaning up... async");
        //    return new ValueTask(Task.CompletedTask);
        //}

        public async Task Run(string[] args)
        {
            _logger.LogInformation("Running.");

            for(var ii = 1; ii <= 10; ii++)
            {
                _logger.LogTrace("... here {counter:00}", ii);

                await Task.Delay(100);

                var now = _timeProvider.NowLocal;

                _logger.LogInformation("Running ... {counter:00} - {now:o}", ii, now);
            }

            _logger.LogInformation("Finished.");
        }
    }
}
