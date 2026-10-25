using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using System;
using System.Collections.Generic;

namespace AI_Powered_Ticketing_and_Support_System.Feature.UserService.Model
{
    public class User
    {
        public Guid Id { get; set; }

        // Microsoft Entra ID these two are not used 
        public string TenantId { get; set; } = string.Empty;
        public string EntraObjectId { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        //using as lastlogin 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Tickets created by this user
        public ICollection<Ticket> CreatedTickets { get; set; }
            = new List<Ticket>();

        // Tickets assigned to this user
        public ICollection<Ticket> AssignedTickets { get; set; }
            = new List<Ticket>();

        public ICollection<Category> Comments { get; set; }
            = new List<Category>();
    }
}