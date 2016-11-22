using System.Diagnostics;

namespace CompareShellExtension.Infrastructure
{
    internal static class Logger
    {
        private const string EventSourceName = AppConstants.FriendlyName;

        public static void LogInformation(string message)
        {
            Log(message, EventLogEntryType.Information);
        }

        public static void LogWarning(string message)
        {
            Log(message, EventLogEntryType.Warning);
        }

        public static void LogError(string message)
        {
            Log(message, EventLogEntryType.Error);
        }

        private static void Log(string message, EventLogEntryType type)
        {
            EventLog.WriteEntry(EventSourceName, message, type);
            Debug.WriteLine(message);
        }

        public static void Register()
        {
            if (!EventLog.SourceExists(EventSourceName))
            {
                EventLog.CreateEventSource(EventSourceName, "Application");
            }
        }

        public static void Unregister()
        {
            if (EventLog.SourceExists(EventSourceName))
            {
                EventLog.DeleteEventSource(EventSourceName);
            }
        }
    }
}