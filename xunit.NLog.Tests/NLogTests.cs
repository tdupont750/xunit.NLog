using System;
using NLog;
using Xunit.Abstractions;

namespace Xunit.NLog.Tests
{
    public class NLogTests : IDisposable
    {
        private readonly ILogger _logger;

        public NLogTests(ITestOutputHelper outputHelper)
        {
            _logger = outputHelper.GetNLogLogger();
        }

        public void Dispose()
        {
            _logger.RemoveTestOutputHelper();
        }

        [Fact]
        public void Hello()
        {
            _logger.Trace("World Trace");
            _logger.Debug("World Debug");
            _logger.Warn("World Warn");
            _logger.Error("World Error");
        }
    }
}
