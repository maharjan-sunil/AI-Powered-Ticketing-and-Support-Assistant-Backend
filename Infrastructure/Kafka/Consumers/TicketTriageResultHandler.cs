using AI_Powered_Ticketing_and_Support_System.Data;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using AI_Powered_Ticketing_and_Support_System_Backend.Events;
using Microsoft.EntityFrameworkCore;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Infrastructure.Kafka.Consumers
{
    public class TicketTriageResultHandler : ITicketTriageResultHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<TicketTriageResultHandler> _logger;

        public TicketTriageResultHandler(
            ApplicationDbContext dbContext,
            ILogger<TicketTriageResultHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task HandleAsync(
            TicketClassifiedEvent ticketEvent,
            CancellationToken cancellationToken)
        {

            Console.WriteLine($"Received TicketClassifiedEvent: TicketId={ticketEvent.TicketId}, Category={ticketEvent.Category}, Priority={ticketEvent.Priority}, Sentiment={ticketEvent.Sentiment}");
            var ticket = await _dbContext.Tickets
                .FirstOrDefaultAsync(
                    x => x.Id == ticketEvent.TicketId,
                    cancellationToken);

            if (ticket == null)
            {
                _logger.LogWarning(
                    "Ticket {TicketId} was not found",
                    ticketEvent.TicketId);

                throw new InvalidOperationException(
                    $"Ticket {ticketEvent.TicketId} not found.");
            }

            //var category = await _dbContext.Categories
            //    .FirstOrDefaultAsync(
            //        x => x.Name == ticketEvent.Category,
            //        cancellationToken);

            //if (category == null)
            //{
            //    throw new InvalidOperationException(
            //        $"Category {ticketEvent.Category} not found.");
            //}

            ticket.CategoryId = (int)Enum.Parse<TicketCategory>(
    ticketEvent.Category,
    ignoreCase: true);
            ticket.Priority = Enum.Parse<Priority>(ticketEvent.Priority, ignoreCase: true);


            //ticket.CategoryId = category.Id;

            //Adapt this assignment to your actual Priority enum/type.
            //if (Enum.TryParse<Priority>(
            //    ticketEvent.Priority,
            //    true,
            //    out var priority))
            //{
            //    ticket.Priority = priority;
            //}
            //else
            //{
            //    throw new InvalidOperationException(
            //        $"Invalid priority: {ticketEvent.Priority}");
            //}

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                            "Ticket {TicketId} updated with AI triage results",
                            ticketEvent.TicketId);
        }
    }
}
