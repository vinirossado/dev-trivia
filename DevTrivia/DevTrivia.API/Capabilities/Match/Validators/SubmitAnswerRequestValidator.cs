using DevTrivia.API.Capabilities.Match.Models;
using FluentValidation;

namespace DevTrivia.API.Capabilities.Match.Validators;

public class SubmitAnswerRequestValidator : AbstractValidator<SubmitAnswerRequest>
{
    public SubmitAnswerRequestValidator()
    {
        RuleFor(x => x.QuestionId)
            .GreaterThan(0).WithMessage("QuestionId must be greater than 0");

        RuleFor(x => x.SelectedAnswerOptionId)
            .GreaterThan(0).When(x => x.SelectedAnswerOptionId.HasValue)
            .WithMessage("SelectedAnswerOptionId must be greater than 0 when provided");
    }
}