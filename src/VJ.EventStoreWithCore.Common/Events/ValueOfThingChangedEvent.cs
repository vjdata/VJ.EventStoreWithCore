using System;

namespace VJ.EventStoreWithCore.Common.Events
{
    public class ValueOfThingChangedEvent : Event
    {
        public int Value { get; }

        public ValueOfThingChangedEvent(Guid id, int value)
        {
            Id = id;
            Value = value;
        }
    }
}
