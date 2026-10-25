using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.UserService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Model;
using System;
using System.Collections.Generic;

namespace AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public long TicketNumber { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;

        public Guid? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public Priority Priority { get; set; } = Priority.Medium;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ResolvedAt { get; set; }

        public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();

        public ICollection<TicketStatusHistory> StatusHistory { get; set; }
            = new List<TicketStatusHistory>();
    }

    public enum TicketStatus : byte
    {
        Open = 0,
        InProgress = 1,
        WaitingForUser = 2,
        Resolved = 3,
        Closed = 4
    }

    public enum Priority : byte
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Critical = 3
    }

    public enum TicketCategory { AccountAccess = 0, Technical = 1, Billing = 2, Network = 3 }
    public enum SentimentType { Positive = 0, Neutral = 1, Negative = 2, Angry = 3, Frustrated = 4 }

    public class TriageReport
    {
        public int CategoryId { get; set; }

        public int Priority { get; set; }
        public int Sentiment { get; set; }
    }

    public class TriageRequest
    {
        public string ticketMessage { get; set; } = string.Empty;
    }
}