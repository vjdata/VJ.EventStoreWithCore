using System;
using System.Collections.Generic;
using System.Net;
using EventStore.ClientAPI;
using VJ.EventStoreWithCore.Common;
using VJ.EventStoreWithCore.Common.Events;

namespace VJ.EventStoreWithCore.Writer
{
    internal class Program
    {
        private static void Main()
        {
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1113));
            connection.ConnectAsync().Wait();

            var aggregateId = Util.GetAggregateId();
            var eventsToRun = new List<Event>
            {
                new ThingCreatedEvent(aggregateId, "Thing #1"),
                new ValueOfThingChangedEvent(aggregateId, 10),
                new ValueOfThingChangedEvent(aggregateId, -5),
                new ValueOfThingChangedEvent(aggregateId, 20),
                new ValueOfThingChangedEvent(aggregateId, 10),
                new ValueOfThingChangedEvent(aggregateId, 25)
            };

            foreach (var @event in eventsToRun)
            {
                connection.AppendToStreamAsync(Util.GetStreamId(aggregateId), ExpectedVersion.Any, @event.GetEventData());
            }

            Console.ReadLine();
        }
    }
}
