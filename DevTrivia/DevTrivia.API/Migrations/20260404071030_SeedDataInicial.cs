using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DevTrivia.API.Migrations;

/// <inheritdoc />
public partial class SeedDataInicial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
            values: new object[,]
            {
                { 100L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "C# and .NET programming", "C#", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "JavaScript and web development", "Javascript", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 102L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Python programming", "Python", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 103L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "SQL and databases", "SQL", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 104L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DevOps, CI/CD and cloud infrastructure", "DevOps", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 105L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Git version control", "Git", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
            });

        migrationBuilder.InsertData(
            table: "Questions",
            columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Difficulty", "Title", "UpdatedAt" },
            values: new object[,]
            {
                { 100L, 100L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Regarding access modifiers in C#", 1, "What is the default access modifier for a class in C#?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 101L, 100L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "About class inheritance in C#", 1, "Which keyword is used to prevent a class from being inherited?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 102L, 100L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Parameter passing in C#", 2, "What is the difference between 'ref' and 'out' parameters?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 103L, 100L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "About iterators in C#", 2, "What does the 'yield' keyword do in C#?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 104L, 100L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Memory-efficient programming in C#", 3, "What is the purpose of the Span<T> type?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 105L, 100L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Memory management in .NET", 4, "How does the .NET garbage collector handle generations?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 106L, 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "JavaScript type quirks", 1, "What is the result of typeof null in JavaScript?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 107L, 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Equality operators in JavaScript", 1, "What is the difference between '==' and '===' in JavaScript?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 108L, 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "JavaScript closures and scope", 2, "What is a closure in JavaScript?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 109L, 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Asynchronous JavaScript", 3, "What is the event loop in JavaScript?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 110L, 102L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Python data structures", 1, "What is the difference between a list and a tuple in Python?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 111L, 102L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Python concurrency", 2, "What does the 'GIL' stand for in Python?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 112L, 102L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Advanced Python features", 2, "What are Python decorators and how do they work?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 113L, 102L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Python metaprogramming", 4, "What is a metaclass in Python?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 114L, 103L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "SQL filtering clauses", 1, "What is the difference between WHERE and HAVING in SQL?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 115L, 103L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "SQL security", 2, "What is a SQL injection and how do you prevent it?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 116L, 103L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "SQL join operations", 1, "What is the difference between INNER JOIN and LEFT JOIN?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 117L, 103L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Advanced SQL features", 3, "What is a window function in SQL?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 118L, 104L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Containerization concepts", 1, "What is the difference between a container and a virtual machine?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 119L, 104L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kubernetes fundamentals", 2, "What is a Kubernetes pod?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 120L, 105L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Git branching strategies", 2, "What is the difference between 'git merge' and 'git rebase'?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 121L, 105L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Advanced git commands", 3, "What does 'git cherry-pick' do?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
            });

        migrationBuilder.InsertData(
            table: "AnswerOptions",
            columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
            values: new object[,]
            {
                { 100L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 100L, "internal", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 101L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 100L, "public", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 102L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 100L, "private", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 103L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 100L, "protected", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 104L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 101L, "sealed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 105L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 101L, "static", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 106L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 101L, "abstract", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 107L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 101L, "readonly", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 108L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 102L, "'out' does not require initialization before being passed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 109L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 102L, "'ref' creates a copy of the variable", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 110L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 102L, "They are exactly the same", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 111L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 102L, "'out' is only for value types", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 112L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 103L, "It returns elements one at a time from an iterator", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 113L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 103L, "It pauses the current thread", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 114L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 103L, "It creates a new task", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 115L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 103L, "It disposes of an object", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 116L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 104L, "A type-safe and memory-safe representation of a contiguous region of memory", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 117L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 104L, "A thread-safe collection type", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 118L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 104L, "A wrapper around StringBuilder", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 119L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 104L, "A type for measuring time intervals", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 120L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 105L, "It uses 3 generations (0, 1, 2) where short-lived objects are collected more frequently", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 121L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 105L, "It uses reference counting like Python", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 122L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 105L, "It collects all objects at the same frequency", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 123L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 105L, "It only runs when memory is completely full", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 124L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 106L, "\"object\"", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 125L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 106L, "\"null\"", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 126L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 106L, "\"undefined\"", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 127L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 106L, "\"boolean\"", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 128L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 107L, "'===' checks value and type without coercion, '==' allows type coercion", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 129L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 107L, "'==' is faster than '==='", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 130L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 107L, "'===' only works with strings", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 131L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 107L, "There is no difference", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 132L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 108L, "A function that retains access to its outer scope's variables even after the outer function has returned", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 133L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 108L, "A way to close a browser window", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 134L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 108L, "A method that prevents garbage collection", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 135L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 108L, "A design pattern for encapsulating classes", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 136L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 109L, "A mechanism that processes callbacks from a queue after the call stack is empty", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 137L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 109L, "A loop that listens for DOM click events", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 138L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 109L, "A multithreading model for parallel execution", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 139L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 109L, "A for loop that runs forever", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 140L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 110L, "Lists are mutable, tuples are immutable", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 141L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 110L, "Tuples are faster but can only hold 2 elements", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 142L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 110L, "Lists can only hold strings", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 143L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 110L, "There is no difference", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 144L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 111L, "Global Interpreter Lock", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 145L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 111L, "General Input Layer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 146L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 111L, "Generic Interface Library", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 147L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 111L, "Global Index Lookup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 148L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 112L, "Functions that modify the behavior of another function without changing its source code", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 149L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 112L, "A way to add CSS styling to Python output", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 150L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 112L, "Special comments that generate documentation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 151L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 112L, "A built-in logging framework", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 152L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 113L, "A class whose instances are classes themselves, controlling class creation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 153L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 113L, "An abstract class that cannot be instantiated", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 154L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 113L, "A class that stores metadata about the program", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 155L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 113L, "A class that only exists in memory", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 156L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 114L, "WHERE filters rows before grouping, HAVING filters groups after aggregation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 157L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 114L, "HAVING is faster than WHERE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 158L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 114L, "WHERE only works with numbers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 159L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 114L, "They are interchangeable", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 160L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 115L, "Malicious SQL inserted via user input; prevented with parameterized queries", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 161L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 115L, "A technique to speed up SQL queries", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 162L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 115L, "Inserting data into multiple tables at once", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 163L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 115L, "A method for database backup", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 164L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 116L, "INNER JOIN returns only matching rows; LEFT JOIN returns all rows from the left table", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 165L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 116L, "LEFT JOIN is always faster", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 166L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 116L, "INNER JOIN can only join two tables", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 167L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 116L, "They produce the same result", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 168L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 117L, "Functions that perform calculations across a set of rows related to the current row without collapsing them", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 169L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 117L, "Functions that only work in windowed applications", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 170L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 117L, "Stored procedures that run on a schedule", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 171L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 117L, "Functions that create temporary tables", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 172L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 118L, "Containers share the host OS kernel; VMs run a full OS with their own kernel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 173L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 118L, "Containers are always more secure than VMs", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 174L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 118L, "VMs are faster to start than containers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 175L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 118L, "Containers require more disk space than VMs", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 176L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 119L, "The smallest deployable unit that can contain one or more containers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 177L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 119L, "A virtual machine managed by Kubernetes", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 178L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 119L, "A network namespace for routing traffic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 179L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 119L, "A storage volume attached to a cluster", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 180L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 120L, "Merge creates a merge commit preserving history; rebase replays commits on top of another branch", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 181L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 120L, "Rebase is always safer than merge", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 182L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 120L, "Merge deletes the source branch automatically", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 183L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 120L, "They produce identical results", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 184L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 121L, "Applies a specific commit from one branch onto another branch", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 185L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 121L, "Selects the best branch to merge", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 186L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 121L, "Reverts the last commit", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 187L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 121L, "Deletes untracked files from the working directory", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 100L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 101L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 102L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 103L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 104L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 105L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 106L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 107L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 108L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 109L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 110L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 111L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 112L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 113L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 114L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 115L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 116L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 117L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 118L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 119L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 120L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 121L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 122L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 123L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 124L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 125L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 126L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 127L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 128L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 129L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 130L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 131L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 132L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 133L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 134L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 135L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 136L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 137L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 138L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 139L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 140L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 141L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 142L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 143L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 144L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 145L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 146L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 147L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 148L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 149L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 150L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 151L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 152L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 153L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 154L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 155L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 156L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 157L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 158L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 159L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 160L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 161L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 162L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 163L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 164L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 165L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 166L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 167L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 168L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 169L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 170L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 171L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 172L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 173L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 174L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 175L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 176L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 177L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 178L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 179L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 180L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 181L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 182L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 183L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 184L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 185L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 186L);

        migrationBuilder.DeleteData(
            table: "AnswerOptions",
            keyColumn: "Id",
            keyValue: 187L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 100L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 101L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 102L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 103L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 104L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 105L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 106L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 107L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 108L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 109L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 110L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 111L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 112L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 113L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 114L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 115L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 116L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 117L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 118L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 119L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 120L);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 121L);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 100L);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 101L);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 102L);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 103L);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 104L);

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: 105L);
    }
}