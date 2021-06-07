using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BomTratoDomain.Events.EscritorioEvents
{
    public class EscritorioEventHandler :
        INotificationHandler<EscritorioRegisteredEvent>,
        INotificationHandler<EscritorioUpdatedEvent>,
        INotificationHandler<EscritorioRemovedEvent>
    {
        public Task Handle(EscritorioRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task Handle(EscritorioRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task Handle(EscritorioUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
