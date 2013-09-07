using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;

/* Contains the eLog stuff for CruiserCommand.
 * Uses a system where a message must have both the required log-level (as most other systems work),
 * but must also match the modules we're currently listening to.
 * This way you wont get spammed by Trace:es from unimportant parts of the program if you set log level to Trace.
 * Use 'all' to listen to every module. Never send a message with module 'all'!
 * Use 'general' for messages that don't belong to any specific module.
 * Errors of level Fatal will be shown regardles of what modules are in the watchlist.
 */
namespace CC.eLog
{
    // Used for fancy logging.
    public class Log : MonoBehaviour
    {
        // List of the modules to print.
        private static List<String> watchlist = new List<String>();

        // The current level to be logged.
        private static ELogLevel logLevel = ELogLevel.Info;

        // Log a module with given level and module.
        public static void Write(ELogLevel level, String module, String message)
        {
            if (level >= logLevel)
            {
                if (watchlist.Contains(module) || watchlist.Contains("all") || level == ELogLevel.Fatal)
                {
                    Debug.Log(FormatEntry(level, module, message));
                }
            }
        }

        // Formats an entry for logging, adding timestamp and stuff.
        private static String FormatEntry(ELogLevel level, String module, String message)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ConstructTimestamp());

            sb.Append("<");
            sb.Append(module);
            sb.Append(">");

            sb.Append("(");
            sb.Append(LogLevelToString(level));
            sb.Append(")\n");

            sb.Append(message);

            return sb.ToString();
        }

        // Constructs the timestamp part of a log entry.
        private static String ConstructTimestamp()
        {
            StringBuilder sb = new StringBuilder();
            DateTime now = DateTime.Now;

            sb.Append("[");
            sb.Append(String.Format("{0:yyyy/MM/dd hh:mm:ss}", now));
            sb.Append("]");

            return sb.ToString();
        }

        // Converts a ELogLevel to the corresponding string.
        private static String LogLevelToString(ELogLevel level)
        {
            switch (level)
            {
                case ELogLevel.Trace:
                    return "TRACE";
                case ELogLevel.Info:
                    return "INFO ";
                case ELogLevel.Error:
                    return "ERROR";
                case ELogLevel.Fatal:
                    return "FATAL";
                default:
                    return "UNKNO";
            }
        }

        // Sets the current log level.
        public static void SetLogLevel(ELogLevel level)
        {
            logLevel = level;
        }

        // Get the current log level.
        public static ELogLevel GetLogLevel()
        {
            return logLevel;
        }

        // Add a module to the watchlist. Space-separate modules to add more than one at a time. Add 'all' to catch every module.
        public static void AddModules(String modules)
        {
            String[] modulesParts = modules.Split(' ');
            foreach (String module in modulesParts)
            {
                watchlist.Add(module);
            }
        }

        // Empites the watchlist and then adds the modules.
        public static void SetModules(String modules)
        {
            EmptyWatchlist();
            AddModules(modules);
        }

        // Empty the watchlist.
        public static void EmptyWatchlist()
        {
            watchlist.Clear();
        }

        // Get the list of modules we're currently listening for.
        public static List<String> GetWatchlist()
        {
            return new List<String>(watchlist);
        }

        // Log a Trace-level message.
        public static void Trace(String module, String message)
        {
            Write(ELogLevel.Trace, module, message);
        }

        // Log a Info-level message.
        public static void Info(String module, String message)
        {
            Write(ELogLevel.Info, module, message);
        }

        // Log an Error-level message.
        public static void Error(String module, String message)
        {
            Write(ELogLevel.Error, module, message);
        }

        // Log a Fatal-level message. Such a message will be shown regardless of the module.
        public static void Fatal(String module, String message)
        {
            Write(ELogLevel.Fatal, module, message);
        }
    }

    public enum ELogLevel : int
    {
        Trace = 0,
        Info = 1,
        Error = 2,
        Fatal = 100
    }
}