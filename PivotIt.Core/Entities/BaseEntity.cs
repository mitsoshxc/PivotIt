using System.Collections.Generic;

namespace PivotIt.Core.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
