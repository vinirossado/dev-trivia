using DevTrivia.API.Capabilities.Match.Enums;
using DevTrivia.API.Capabilities.Match.Models;
using FluentValidation;

namespace DevTrivia.API.Capabilities.Match.Validators;

public class MatchRequestValidator : AbstractValidator<MatchRequest>
{
    public MatchRequestValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status must be a valid value (1=Pending, 2=InProgress, 3=Finished)");

        RuleFor(x => x.SelectedCategoryId)
            .GreaterThan(0).WithMessage("SelectedCategoryId must be greater than 0");

        RuleFor(x => x.StartedAt)
            .NotEmpty().WithMessage("StartedAt is required");

        RuleFor(x => x.EndedAt)
            .NotEmpty().WithMessage("EndedAt is required")
            .GreaterThanOrEqualTo(x => x.StartedAt).WithMessage("EndedAt must be after StartedAt");
    }
}
