using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;

namespace JanusEngine.Trace
{
    [EventSource(Name ="Eymike-JanusEngine", Guid = "043E298D-D48B-4FF7-865A-24081BCD28DF")]
    public sealed class Logger : EventSource
    {
        public static readonly Logger Log = new Logger();

        [Event(1)]
        public void Info(string message)
        {
            WriteEvent(1, message);
        }

        [Event(2)]
        public void Warning(string message)
        {
            WriteEvent(2, message);
        }

        [Event(3)]
        public void Error(string message)
        {
            WriteEvent(3, message);
        }

        [Event(4)]
        public void Fatal(string message)
        {
            WriteEvent(4, message);
        }
        
    }
}
