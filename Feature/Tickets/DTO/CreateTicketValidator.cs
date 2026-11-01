using FluentValidation;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Feature.Tickets.DTO
{
    public class CreateTicketValidator : AbstractValidator<TicketCreateDto>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.ticketMessage)
                .NotEmpty().WithMessage("Ticket Message is required");
        }
    }
}
