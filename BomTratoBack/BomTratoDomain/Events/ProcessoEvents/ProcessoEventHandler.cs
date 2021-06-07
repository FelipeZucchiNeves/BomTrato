using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BomTratoDomain.Events.ProcessoEvents
{
    public class ProcessoEventHandler :
        INotificationHandler<ProcessoRegisteredEvent>,
        INotificationHandler<ProcessoUpdatedEvent>,
        INotificationHandler<ProcessoRemovedEvent>
    {
        public Task Handle(ProcessoRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProcessoRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProcessoUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
