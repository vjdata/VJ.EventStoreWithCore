using System;
using System.Net;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using VJ.EventStoreWithCore.Common;
using VJ.EventStoreWithCore.Common.Domain;
using VJ.EventStoreWithCore.Common.Events;

namespace VJ.EventStoreWithCore.Reader
{
    internal class Program
    {
        private static void Main()
        {
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1113));
            connection.ConnectAsync().Wait();

            var aggregateId = Util.GetAggregateId();
            var results = Task.Run(() => connection.ReadStreamEventsForwardAsync(Util.GetStreamId(aggregateId), StreamPosition.Start, 10, false));
            Task.WaitAll();

            var resultsData = results.Result;

            var thing = new Thing();

            foreach (var evnt in resultsData.Events)
            {
                if (evnt.Event.EventType == "ThingCreatedEvent")
                {
                    var objState = evnt.Event.ParseJson<ThingCreatedEvent>();
                    thing.Apply(objState);
                }
                else
                {
                    var objState = evnt.Event.ParseJson<ValueOfThingChangedEvent>();
                    thing.Apply(objState);
                }
                Console.WriteLine($"{thing.Name}s Value was: {thing.Value}");
            }

            Console.WriteLine($"{Environment.NewLine}Currently {thing.Name}s Value is: {thing.Value}");
            Console.ReadLine();
        }
    }
}
