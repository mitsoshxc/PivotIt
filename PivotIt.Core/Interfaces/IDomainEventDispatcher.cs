using PivotIt.Core.Entities;
using System.Threading.Tasks;

namespace PivotIt.Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(BaseDomainEvent domainEvent);
    }
}
