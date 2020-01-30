using System;

namespace PivotIt.Core.Interfaces
{
    public interface IDomainEvent
    {
        DateTime EventDateOccurred { get; }
    }
}
