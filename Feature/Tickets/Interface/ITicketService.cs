using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;

namespace AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Interface
{
    public interface ITicketService
    {
        public Task<IEnumerable<Ticket>> GetTicketsAsync();
        public Task AddTicketAsync(Ticket ticket);
        public Task<bool> DeleteTicketAsync(Guid id);
        public Task<Ticket> GetTicketByIdAsync(Guid ticketId);
        public Task<Ticket?> UpdateTicketAsync(Ticket ticket);
    }
}
