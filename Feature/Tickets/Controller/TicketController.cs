using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;
using AI_Powered_Ticketing_and_Support_System.Feature.UserService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
//using UserService.API.RabbitMQ;

namespace AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Controller
{
   //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly ITicketService  _service;
        public TicketController(ITicketService service) => _service = service;

        [HttpGet]
        public  async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _service.GetTicketsAsync();
            return Ok(tickets);
        }
       
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddTicket(Ticket ticket)
        {
            // 1. Save as Pending (simulate DB)
            ticket.Status = TicketStatus.Open;

            // 2. Send message to RabbitMQ
            //var factory = new ConnectionFactory() { HostName = "localhost" };

            //using var connection = factory.CreateConnection();
            //using var channel = connection.CreateModel();

            //channel.QueueDeclare("store-queue", true, false, false);

            //var message = JsonSerializer.Serialize(store);
            //var body = Encoding.UTF8.GetBytes(message);

            //channel.BasicPublish("", "store-queue", null, body);

            await _service.AddTicketAsync(ticket);
            return Ok();

        }

        [HttpPatch]
       // [Authorize]
        public async Task<IActionResult> UpdateStore(Ticket ticket)
        {
            if(ticket is null)
                return BadRequest("Ticket cannot be null.");

            await _service.UpdateTicketAsync(ticket);
            return Ok(ticket);

        }

        [HttpDelete("{ticketId:guid}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteTicket(Guid ticketId)
        {
            if (ticketId == Guid.Empty)
                return BadRequest("Ticket ID cannot be empty.");

            var deleted = await _service.DeleteTicketAsync(ticketId);

            if (!deleted)
                return NotFound($"Ticket with ID {ticketId} not found.");

            return NoContent();

        }

       
        [HttpGet("search")]
        //[Authorize]
        public async Task<ActionResult<Ticket>> GetTicketById(Guid ticketId)
        {
            if (ticketId == Guid.Empty)
                return BadRequest("Ticket ID cannot be empty.");

            var ticket = await _service.GetTicketByIdAsync(ticketId);

            if (ticket is null)
                return NotFound($"Ticket with ID {ticketId} not found.");

            return Ok(ticket);
        }
    }
}

