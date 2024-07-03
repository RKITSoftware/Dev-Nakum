using NLog.Config;
using NLog.Targets;
using NLog;

namespace Logging.BL
{
    /// <summary>
    /// add the nlog configuration dynamically
    /// </summary>
    public static class BLDynamicNlogConfig
    {
        public static void AddDynamicFileTarget(string targetName, string fileName)
        {
            var config = LogManager.Configuration ?? new LoggingConfiguration();
            fileName = "${currentdir}/logs/" + fileName + ".log";

            // Check if the target already exists
            var existingTarget = config.FindTargetByName(targetName) as FileTarget;
            if (existingTarget != null)
            {
                existingTarget.FileName = fileName;
            }
            else
            {
                // Create a new file target
                var fileTarget = new FileTarget(targetName)
                {
                    FileName = fileName,
                    Layout = "${longdate} ${uppercase:${level}} ${message}"
                };

                // Add the new file target to the configuration
                config.AddTarget(fileTarget);

                // Define a new rule for the file target
                var rule = new LoggingRule("dynamic", NLog.LogLevel.Info, NLog.LogLevel.Info, fileTarget);
                config.LoggingRules.Add(rule);
            }

            // Apply the new configuration
            LogManager.Configuration = config;

            // Reconfigure existing loggers
            LogManager.ReconfigExistingLoggers();


        }
    }
}
