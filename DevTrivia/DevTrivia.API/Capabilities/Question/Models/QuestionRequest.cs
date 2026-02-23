﻿using DevTrivia.API.Capabilities.Question.Enums;

namespace DevTrivia.API.Capabilities.Question.Models;

public record QuestionRequest
{
    public required string Title { get; init; } = string.Empty;
    public required string Description { get; init; } = string.Empty;
    public required DifficultyEnum Difficulty { get; init; }
    public long CategoryId { get; init; }
}
