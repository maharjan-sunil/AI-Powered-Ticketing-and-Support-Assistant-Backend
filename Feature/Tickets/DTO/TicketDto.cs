namespace AI_Powered_Ticketing_and_Support_System_Backend.Feature.Tickets.DTO
{
    public class TicketDto
    {
        public Guid Id { get; set; }

        public long TicketNumber { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Priority { get; set; }

        public string Category { get; set; }
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
