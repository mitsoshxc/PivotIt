using Autofac;
using PivotIt.Core.Entities;
using PivotIt.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PivotIt.Infrastructure.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IComponentContext _container;

        public DomainEventDispatcher(IComponentContext container)
        {
            _container = container;
        }

        public async Task Dispatch(BaseDomainEvent domainEvent)
        {
            var wrappedHandlers = GetWrappedHandlers(domainEvent);

            foreach (var handler in wrappedHandlers)
            {
                await handler.Handle(domainEvent).ConfigureAwait(false);
            }
        }

        private IEnumerable<DomainEventHandler> GetWrappedHandlers(BaseDomainEvent domainEvent)
        {
            var handlerType = typeof(IHandle<>).MakeGenericType(domainEvent.GetType());
            var wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());

            var handlers = (IEnumerable)_container.Resolve(typeof(IEnumerable<>).MakeGenericType(handlerType));
            var wrappedHandlers = handlers.Cast<object>().Select(handlers => (DomainEventHandler)Activator.CreateInstance(wrapperType, handlers));

            return wrappedHandlers;
        }
    }
}
