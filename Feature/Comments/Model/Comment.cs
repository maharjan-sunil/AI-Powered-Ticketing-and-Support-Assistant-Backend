using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.UserService.Model;

namespace AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Model
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;

        public Guid AuthorUserId { get; set; }
        public User AuthorUser { get; set; } = null!;

        public string Message { get; set; } = string.Empty;

        // True means the comment is an internal agent note
        public bool IsInternal { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}