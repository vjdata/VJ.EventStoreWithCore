using System;

namespace VJ.EventStoreWithCore.Common.Events
{
    public class ThingCreatedEvent : Event
    {
        public string Name { get; }

        public ThingCreatedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
