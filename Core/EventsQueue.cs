using System;
using System.Collections.Concurrent;
using log4net.Core;

namespace Cirrocumulus
{
    public class EventsQueue
    {
        public static EventsQueue Instance
        {
            get { return instance.Value; }
        }

        private static Lazy<EventsQueue> instance = new Lazy<EventsQueue>();

        private EventsQueue()
        {
            queue = new ConcurrentQueue<LoggingEvent>();
        }

        private ConcurrentQueue<LoggingEvent> queue;
    }
}
