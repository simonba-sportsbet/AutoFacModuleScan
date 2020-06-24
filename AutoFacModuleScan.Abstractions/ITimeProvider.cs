using System;

namespace AutoFacModuleScan.Abstractions
{
    public interface ITimeProvider
    {
        DateTime NowUtc { get; }
        DateTimeOffset NowLocal { get; }
    }
}
