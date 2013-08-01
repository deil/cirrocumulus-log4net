using System;
using System.Collections.Concurrent;
using log4net.Core;
using System.Threading;
using System.ServiceModel;

namespace Cirrocumulus
{
    public class EventsQueue
    {
        public static EventsQueue Instance
        {
            get { return instance.Value; }
        }

        public void Enqueue(LoggingEvent @event)
        {
            _queue.Enqueue(@event);
            _waitHandle.Set();
        }

        public void Close()
        {
            Console.WriteLine("EventsQueue::Close()");
            _shouldStop = true;
            _thread.Join();
        }

        #region private

        private static readonly Lazy<EventsQueue> instance = new Lazy<EventsQueue>(() => new EventsQueue());

        private EventsQueue()
        {
            _queue = new ConcurrentQueue<LoggingEvent>();
            _waitHandle = new AutoResetEvent(false);
            _shouldStop = false;

            _factory = new ChannelFactory<ICollectorService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8129/collector"));

            _thread = new Thread(() => ProcessQueue()) { IsBackground = true };
            _thread.Start();
        }

        private volatile ConcurrentQueue<LoggingEvent> _queue;
        private volatile AutoResetEvent _waitHandle;
        private volatile bool _shouldStop;
        private Thread _thread;
        private ChannelFactory<ICollectorService> _factory;

        private void ProcessQueue()
        {
            while (!_shouldStop)
            {
                if (_queue.IsEmpty)
                {
                    _waitHandle.WaitOne();
                }

                LoggingEvent item;

                while (_queue.TryDequeue(out item))
                {
                    try
                    {
                        ProcessItem(item);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

            Console.WriteLine("Stopped");
        }

        private void ProcessItem(LoggingEvent item)
        {
            try
            {
                Console.WriteLine("Opening");
                _factory.Open();
                var client = _factory.CreateChannel();
                Console.WriteLine(client.SendRawEvent(item.TimeStamp, item.LoggerName, item.RenderedMessage));
            }
            finally
            {
                _factory.Close();
            }
        }

        #endregion
    }
}
