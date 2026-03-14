using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Category.Models;

namespace DevTrivia.API.Capabilities.Category.Extensions;

public static class CategoryMappingExtensions
{
    public static CategoryResponse ToResponse(this CategoryEntity entity)
    {
        return new CategoryResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt.GetValueOrDefault(),
            UpdatedAt = entity.UpdatedAt.GetValueOrDefault()
        };
    }

    public static CategoryEntity ToEntity(this CategoryRequest request)
    {
        return new CategoryEntity
        {
            Name = request.Name,
            Description = request.Description
        };
    }

    public static IEnumerable<CategoryResponse> ToResponseList(this IEnumerable<CategoryEntity> entities)
    {
        return entities.Select(e => e.ToResponse());
    }
}
