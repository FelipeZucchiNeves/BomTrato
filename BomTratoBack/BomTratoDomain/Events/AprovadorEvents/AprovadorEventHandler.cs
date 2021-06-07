using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BomTratoDomain.Events.AprovadorEvents
{
    public class AprovadorEventHandler :
        INotificationHandler<AprovadorRegisteredEvent>,
        INotificationHandler<AprovadorUpdatedEvent>,
        INotificationHandler<AprovadorRemovedEvent>
    {
        public Task Handle(AprovadorRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task Handle(AprovadorRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task Handle(AprovadorUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
