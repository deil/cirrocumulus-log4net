using System;
using log4net.Appender;
using log4net.Core;

namespace Cirrocumulus
{
    public class CirrocumulusAppender : IAppender
    {
        public CirrocumulusAppender()
        {
        }

        public string Name { get; set; }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            return;
        }

        public void Close()
        {
        }
    }
}
