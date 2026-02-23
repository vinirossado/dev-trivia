using DevTrivia.API.Capabilities.Question.Models;
using FluentValidation;

namespace DevTrivia.API.Capabilities.Question.Validators;

public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
{
    private static readonly string[] ValidDifficulties = { "easy", "medium", "hard" };

    public QuestionRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Question title is required")
            .MaximumLength(255).WithMessage("Title must not exceed 255 characters")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(255).WithMessage("Description must not exceed 255 characters")
            .MinimumLength(10).WithMessage("Description must be at least 10 characters");

        RuleFor(x => x.Difficulty)
            .NotEmpty().WithMessage("Difficulty is required")
            .Must(d => ValidDifficulties.Contains(d.ToLower()))
            .WithMessage($"Difficulty must be one of: {string.Join(", ", ValidDifficulties)}");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Valid CategoryId is required");
    }
}
