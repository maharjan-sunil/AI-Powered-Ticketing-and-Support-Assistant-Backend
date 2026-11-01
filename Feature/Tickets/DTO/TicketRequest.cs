namespace AI_Powered_Ticketing_and_Support_System_Backend.Feature.Tickets.DTO
{
    public record TicketCreateDto(string ticketMessage);
    public record TicketUpdateDto(Guid ticketId, string ticketMessage);
}
