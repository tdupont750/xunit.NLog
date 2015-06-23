using NLog;
using Xunit.Abstractions;
using Xunit.NLog.Helpers;

// ReSharper disable once CheckNamespace
namespace Xunit
{
    public static class NLogTestOutputExtensions
    {
        public static ILogger GetNLogLogger(
            this ITestOutputHelper testOutputHelper,
            string loggerName = null,
            bool addNumericSuffix = true)
        {
            var addedName = TestOutputHelpers.AddTestOutputHelper(
                testOutputHelper,
                loggerName,
                addNumericSuffix);

            return LogManager.GetLogger(addedName);
        }

        public static void RemoveTestOutputHelper(this ILogger logger)
        {
            TestOutputHelpers.RemoveTestOutputHelper(logger.Name);
        }
    }
}