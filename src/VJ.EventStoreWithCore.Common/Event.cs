using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace VJ.EventStoreWithCore.Common
{
    public interface IEvent
    {
        Guid Id { get; }
    }

    public class Event : IEvent {
        public Guid Id { get; set; }

        public string GetJsonString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });
        }

        public byte[] GetJsonByteArray()
        {
            return Encoding.UTF8.GetBytes(GetJsonString());
        }

        public EventData GetEventData()
        {
            return new EventData(Guid.NewGuid(), GetType().Name, true, GetJsonByteArray(), new byte[] { });
        }
    }
}
