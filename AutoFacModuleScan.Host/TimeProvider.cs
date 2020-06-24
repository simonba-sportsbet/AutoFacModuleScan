using System;
using AutoFacModuleScan.Abstractions;

namespace AutoFacModuleScan.Host
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime NowUtc => DateTime.UtcNow;

        public DateTimeOffset NowLocal => DateTimeOffset.Now;
    }
}
