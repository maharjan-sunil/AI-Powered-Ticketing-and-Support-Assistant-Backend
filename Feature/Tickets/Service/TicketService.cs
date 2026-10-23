using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using AI_Powered_Ticketing_and_Support_System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UserService.API.Security;

namespace AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Service
{
    public class TicketService : ITicketService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<TicketService> _logger;
        //  private readonly AzureKey _key;

        public TicketService(ApplicationDbContext context, ILogger<TicketService> logger)
        {
            _context = context;
            //    _key = key;
            _logger = logger;
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            //   string encyrpted = await _key.Encrypt(ticket.Location);
            //string decyrpted = await _key.Decrypt(encyrpted);
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTicketAsync(Guid id)
        {

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket is null)
                return false;

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid ticketId)
        {

            return await _context.Tickets
            .Where(s => s.Id.Equals(ticketId))
            .FirstAsync();
        }

        public async Task<Ticket?> UpdateTicketAsync(Ticket ticket)
        {
            var ticketDetail = await _context.Tickets
                .FindAsync(ticket.Id);

            if (ticketDetail is null)
                return null;

            ticketDetail.Title = ticket.Title;
            ticketDetail.Description = ticket.Description;
            ticketDetail.Priority = ticket.Priority;
            ticketDetail.Status = ticket.Status;

            try
            {
                await _context.SaveChangesAsync();
                return ticketDetail;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
