using AI_Powered_Ticketing_and_Support_System_Backend.Events;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Infrastructure.Kafka.Producers
{
    public interface ITicketCreatedProducer
    {
        Task PublishAsync(
        TicketCreatedEvent ticketEvent,
        CancellationToken cancellationToken = default);
    }
}
