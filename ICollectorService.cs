using System;
using System.ServiceModel;

namespace Cirrocumulus
{
    [ServiceContract]
    public interface ICollectorService
    {
        [OperationContract]
        bool SendRawEvent(DateTime time, string origin, string @event);
    }
}
