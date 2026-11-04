using AI_Powered_Ticketing_and_Support_System.Data;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using AI_Powered_Ticketing_and_Support_System_Backend.Feature.Tickets.DTO;
using AI_Powered_Ticketing_and_Support_System_Backend.Middleware;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using UserService.API.Security;

namespace AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Service
{
    public class TicketService : ITicketService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<TicketService> _logger;
        private readonly ICurrentUserService _currentUserService;

        private readonly TicketAiService _ticketAiService;
        //  private readonly AzureKey _key;

        public TicketService(ApplicationDbContext context,
            ILogger<TicketService> logger,
            TicketAiService ticketAiService,
            ICurrentUserService currentUserService
            )
        {
            _context = context;
            //    _key = key;
            _logger = logger;
            _ticketAiService = ticketAiService;
            _currentUserService = currentUserService;
        }

        public async Task<TicketDto> AddTicketAsync(TicketCreateDto request)
        {

            var requestDto = new TriageRequest
            {
              ticketMessage = request.ticketMessage
            };

           
            TriageReport? result = await GetTriageReport(requestDto);

            var ticketEntity = new Ticket
            {
                Id = Guid.NewGuid(),
                Description = request.ticketMessage.Trim(),
                Status = TicketStatus.Open,
                CreatedAt = DateTime.Now,

                /* Pending task retrieve the logged in user detail */
                CreatedByUserId = _currentUserService.UserId,
                CategoryId = (int)(TicketCategory)result.Category,
                Priority = (Priority)result.Urgency,
            };
            //   string encyrpted = await _key.Encrypt(ticket.Location);
            //string decyrpted = await _key.Decrypt(encyrpted);

            _context.Tickets.Add(ticketEntity);
            await _context.SaveChangesAsync();

            var ticketDto = ConvertToDto(ticketEntity);
            return ticketDto;
        }

        private async Task<TriageReport> GetTriageReport(TriageRequest requestDto)
        {
            var result = await _ticketAiService.AnalyzeTicketAsync(requestDto);
            return result;
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

        public async Task<IEnumerable<TicketDto>> GetTicketsAsync()
        {
            var databaseTickets = await _context.Tickets.ToListAsync();

            var clientTickets = new List<TicketDto>();

            foreach (var dbTicket in databaseTickets)
            {
                var dto = ConvertToDto(dbTicket);
                clientTickets.Add(dto);
            }
            return clientTickets.OrderByDescending(t=>t.CreatedAt);
        }

        public async Task<TicketDto> GetTicketByIdAsync(Guid ticketId)
        {

            var ticket = await _context.Tickets
            .Where(s => s.Id.Equals(ticketId))
            .FirstAsync();

            var ticketDto = ConvertToDto(ticket);

            return ticketDto;
        }

        public async Task<Ticket?> UpdateTicketAsync(TicketUpdateDto ticket)
        {
            var ticketDetail = await _context.Tickets
                .FindAsync(ticket.ticketId);

            if (ticketDetail is null)
                return null;

            var requestDto = new TriageRequest
            {
                ticketMessage = ticket.ticketMessage
            };

            //ticketDetail.Title = ticket.Title;
            ticketDetail.Description = ticket.ticketMessage;
            TriageReport? result = await GetTriageReport(requestDto);
            ticketDetail.CategoryId = (int)(TicketCategory)result.Category;
            ticketDetail.Priority = (Priority)result.Urgency;

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

        public TicketDto ConvertToDto (Ticket dbTicket)
        {
            var dto = new TicketDto
            {
                Id = dbTicket.Id,
                TicketNumber = dbTicket.TicketNumber,
                Title = dbTicket.Title,
                Description = dbTicket.Description,
                CreatedAt = dbTicket.CreatedAt,
                Priority = ((Priority)dbTicket.Priority).ToString(),
                Status = ((TicketStatus)dbTicket.Status).ToString(),
                Category = ((TicketCategory)dbTicket.CategoryId).ToString()
            };

            return dto;
        }
    }
}
