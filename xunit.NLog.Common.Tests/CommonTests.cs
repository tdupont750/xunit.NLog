using System;
using Common.Logging;
using Xunit.Abstractions;

namespace Xunit.NLog.Common.Tests
{
    public class CommonTests : IDisposable
    {
        private readonly ILog _log;

        public CommonTests(ITestOutputHelper outputHelper)
        {
            _log = outputHelper.GetCommonLog();
        }

        public void Dispose()
        {
            _log.RemoveTestOutputHelper();
        }

        [Fact]
        public void Goodnight()
        {
            _log.Trace("Moon Trace");
            _log.Debug("Moon Debug");
            _log.Warn("Moon Warn");
            _log.Error("Moon Error");
        }
    }
}
