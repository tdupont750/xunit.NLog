using System.Linq;
using System.Threading;
using NLog;
using Xunit.Abstractions;
using Xunit.NLog.Targets;

namespace Xunit.NLog.Helpers
{
    public static class TestOutputHelpers
    {
        public const string DefaultLoggerName = "Test";

        private static int _loggerId;

        public static string AddTestOutputHelper(
            ITestOutputHelper testOutputHelper,
            string loggerName,
            bool addNumericSuffix)
        {
            var targets = LogManager.Configuration.AllTargets
                .OfType<TestOutputTarget>();

            if (string.IsNullOrWhiteSpace(loggerName))
                loggerName = DefaultLoggerName;

            if (addNumericSuffix)
                loggerName += Interlocked.Increment(ref _loggerId);

            foreach (var target in targets)
                target.Add(testOutputHelper, loggerName);

            return loggerName;
        }

        public static void RemoveTestOutputHelper(string name)
        {
            var targets = LogManager.Configuration.AllTargets
                .OfType<TestOutputTarget>();

            foreach (var target in targets)
                target.Remove(name);
        }
    }
}
