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
            EventsQueue.Instance.Enqueue(loggingEvent);
        }

        public void Close()
        {
            EventsQueue.Instance.Close();
        }
    }
}
