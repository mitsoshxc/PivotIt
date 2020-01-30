using PivotIt.Core.Entities;
using System.Threading.Tasks;

namespace PivotIt.Core.Interfaces
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
        Task Handle(T domainEvent);
    }
}
