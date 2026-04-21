using SmallApi_ExtensionMethods.Models;

public static class CategoryEndpoints
{
    private static readonly List<Category> Categories =
    [
        new(1, "Personal"),
        new(2, "Work"),
        new(3, "Health"),
    ];

    private static int _nextId = 4;

    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/categories");

        group.MapGet("/", () => Results.Ok(Categories));

        group.MapGet("/{id:int}", (int id) =>
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            return category is not null ? Results.Ok(category) : Results.NotFound();
        });

        group.MapPost("/", (CreateCategoryRequest request) =>
        {
            var category = new Category(_nextId++, request.Name);
            Categories.Add(category);
            return Results.Created($"/api/categories/{category.Id}", category);
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            var index = Categories.FindIndex(c => c.Id == id);
            if (index == -1) return Results.NotFound();

            Categories.RemoveAt(index);
            return Results.NoContent();
        });
    }

    private record CreateCategoryRequest(string Name);
}
