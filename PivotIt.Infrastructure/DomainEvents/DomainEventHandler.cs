using PivotIt.Core.Entities;
using PivotIt.Core.Interfaces;
using System.Threading.Tasks;

namespace PivotIt.Infrastructure.DomainEvents
{
    public abstract class DomainEventHandler
    {
        public abstract Task Handle(BaseDomainEvent domainEvent);
    }

    public class DomainEventHandler<T> : DomainEventHandler
            where T : BaseDomainEvent
    {
        private readonly IHandle<T> _handler;

        public DomainEventHandler(IHandle<T> handler)
        {
            _handler = handler;
        }

        public override Task Handle(BaseDomainEvent domainEvent)
        {
            return _handler.Handle((T)domainEvent);
        }
    }
}
