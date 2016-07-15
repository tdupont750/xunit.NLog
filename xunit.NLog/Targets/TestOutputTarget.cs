using System;
using System.Collections.Concurrent;
using NLog;
using NLog.Targets;
using Xunit.Abstractions;

namespace Xunit.NLog.Targets
{
    [Target("TestOutput")]
    public class TestOutputTarget : TargetWithLayoutHeaderAndFooter
    {
        private readonly ConcurrentDictionary<string, ITestOutputHelper> _map
            = new ConcurrentDictionary<string, ITestOutputHelper>();

        public void Add(ITestOutputHelper testOutputHelper, string loggerName)
        {
            if (string.IsNullOrWhiteSpace(loggerName))
                throw new ArgumentNullException(nameof(loggerName));

            if (_map.TryAdd(loggerName, testOutputHelper))
                return;

            throw new ArgumentException(
                "LoggerName already in use",
                nameof(loggerName));
        }

        public bool Remove(string loggerName)
        {
            if (string.IsNullOrWhiteSpace(loggerName))
                throw new ArgumentNullException(nameof(loggerName));

            ITestOutputHelper testOutputHelper;
            return _map.TryRemove(loggerName, out testOutputHelper);
        }

        protected override void Write(LogEventInfo logEvent)
        {
            ITestOutputHelper testOutputHelper;
            if (!_map.TryGetValue(logEvent.LoggerName, out testOutputHelper))
                return;

            try
            {
                var message = Layout.Render(logEvent);
                testOutputHelper.WriteLine(message);
            }
            catch (Exception ex)
            {
                // If NLog is set to async, and we try to write to a test 
                // that has been uninitialized, then it will throw.

                var logger = LogManager.GetCurrentClassLogger();
                logger.Debug("TestOutputTarget.Write - Exception: {0}", ex.Message);
            }
        }
    }
}