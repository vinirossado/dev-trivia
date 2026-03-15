using DevTrivia.API.Capabilities.AnswerOptions.Models;
using FluentValidation;

namespace DevTrivia.API.Capabilities.AnswerOptions.Validators;

public class AnswerOptionRequestValidator : AbstractValidator<AnswerOptionRequest>
{
    public AnswerOptionRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Option Text is required")
            .MinimumLength(5).WithMessage("Text must be at least 5 characters")
            .MaximumLength(255).WithMessage("Text must not exceed 255 characters");

        RuleFor(x => x.QuestionId)
            .GreaterThan(0)
            .WithMessage("QuestionId must be a valid value");

    }
}