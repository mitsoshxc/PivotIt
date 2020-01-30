using PivotIt.Core.Interfaces;
using System;

namespace PivotIt.Core.Entities
{
    public abstract class BaseDomainEvent : IDomainEvent
    {
        public DateTime EventDateOccurred { get; } = DateTime.UtcNow;
    }
}
