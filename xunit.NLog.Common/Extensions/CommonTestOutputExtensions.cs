using System.Collections.Concurrent;
using Common.Logging;
using Xunit.Abstractions;
using Xunit.NLog.Helpers;

// ReSharper disable once CheckNamespace
namespace Xunit
{
    public static class CommonTestOutputExtensions
    {
        private static readonly ConcurrentDictionary<int, string> CommonMap
            = new ConcurrentDictionary<int, string>();

        public static ILog GetCommonLog(
            this ITestOutputHelper testOutputHelper,
            string loggerName = null,
            bool addNumericSuffix = true)
        {
            var addedName = TestOutputHelpers.AddTestOutputHelper(
                testOutputHelper,
                loggerName,
                addNumericSuffix);

            var log = LogManager.GetLogger(addedName);

            var commonKey = log.GetHashCode();
            CommonMap.TryAdd(commonKey, addedName);

            return log;
        }

        public static void RemoveTestOutputHelper(this ILog log)
        {
            var commonKey = log.GetHashCode();

            string name;
            if (CommonMap.TryRemove(commonKey, out name))
                TestOutputHelpers.RemoveTestOutputHelper(name);
        }
    }
}