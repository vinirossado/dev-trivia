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
    }
}
