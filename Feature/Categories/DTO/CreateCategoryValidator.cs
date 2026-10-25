using FluentValidation;
namespace AI_Powered_Ticketing_and_Support_System.Feature.Categories.DTO;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters.")
            .Must(name => !name.Contains("<script>")).WithMessage("Invalid characters detected in name."); // Simple XSS safety

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}
