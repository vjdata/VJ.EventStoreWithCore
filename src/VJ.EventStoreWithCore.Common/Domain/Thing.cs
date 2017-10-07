using System;
using System.Collections.Generic;
using VJ.EventStoreWithCore.Common.Events;

namespace VJ.EventStoreWithCore.Common.Domain
{
    public class Thing
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public IList<int> PastValueChanges = new List<int>();

        public void Apply(ThingCreatedEvent @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Value = 0;

            PastValueChanges.Add(Value);
        }

        public void Apply(ValueOfThingChangedEvent @event)
        {
            PastValueChanges.Add(@event.Value);
            Value = Value + @event.Value;
        }
    }
}
