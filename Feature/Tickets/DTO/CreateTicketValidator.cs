using FluentValidation;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Feature.Tickets.DTO
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.ticketMessage)
                .NotEmpty().WithMessage("Ticket Message is required");
        }
    }
}
