using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastopedia
{
    public static class LogManager
    {
        static List<LogEventClass> LogEvents = new List<LogEventClass>();


        public static void Log(string message, string stacktrace, string level, string codelog)
        {
            LogEventClass logEventClass = new LogEventClass();
            logEventClass.Message = message;
            logEventClass.Level = level;
            logEventClass.CodeLog = codelog;
            logEventClass.StackTrace = stacktrace;
            LogEvents.Add(logEventClass);
        }

        public static void Log(Exception exception)
        {
            Log(exception.Message, exception.StackTrace, "Error", "CODE_A_" + exception.HResult.ToString());
        }

        public static List<LogEventClass> GetLogEvents()
        {
            return LogEvents.ToList();
        }
    }
}
