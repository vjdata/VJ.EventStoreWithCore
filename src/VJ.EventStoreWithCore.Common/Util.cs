using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace VJ.EventStoreWithCore.Common
{
    public static class Util
    {
        public static Guid GetAggregateId()
        {
            return new Guid("BEEF8CB0-7890-4E05-B003-55470951F1A8");
        }

        public static string GetStreamId(Guid id)
        {
            return string.Format($"Thing-{id}");
        }

        public static T ParseJson<T>(this RecordedEvent data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var value = Encoding.UTF8.GetString(data.Data);

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
