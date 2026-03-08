using DevTrivia.API.Capabilities.Match.Models;
using FluentValidation;

namespace DevTrivia.API.Capabilities.Match.Validators;

public class MatchRequestValidator : AbstractValidator<MatchRequest>
{
    public MatchRequestValidator()
    {
        //RuleFor(x => x.Name)
        //    .NotEmpty().WithMessage("Category name is required")
        //    .MaximumLength(100).WithMessage("Category name must not exceed 100 characters")
        //    .MinimumLength(2).WithMessage("Category name must be at least 2 characters");

        //RuleFor(x => x.Description)
        //    .NotEmpty().WithMessage("Description is required")
        //    .MaximumLength(255).WithMessage("Description must not exceed 255 characters")
        //    .MinimumLength(10).WithMessage("Description must be at least 10 characters");
    }
}
