using AI_Powered_Ticketing_and_Support_System.Feature.UserService.Model;

namespace AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model
{
    public class TicketStatusHistory
    {
        public long Id { get; set; }

        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;

        public Guid ChangedByUserId { get; set; }
        public User ChangedByUser { get; set; } = null!;

        public TicketStatus OldStatus { get; set; }
        public TicketStatus NewStatus { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}