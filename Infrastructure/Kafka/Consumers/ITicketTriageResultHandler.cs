using AI_Powered_Ticketing_and_Support_System_Backend.Events;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Infrastructure.Kafka.Consumers
{
    public interface ITicketTriageResultHandler
    {
        Task HandleAsync(
        TicketClassifiedEvent ticketEvent,
        CancellationToken cancellationToken);
    }
}
