using SmallApi_ExtensionMethods.Models;

public static class TodoEndpoints
{
    private static readonly List<TodoItem> Todos =
    [
        new(1, "Buy groceries", 1, false),
        new(2, "Write blog post", 2, true),
        new(3, "Fix login bug", 2, false),
        new(4, "Schedule dentist appointment", 1, false),
    ];

    private static int _nextId = 5;

    public static void MapTodoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/todos");

        group.MapGet("/", (int? categoryId) =>
        {
            return categoryId is null
                ? Results.Ok(Todos)
                : Results.Ok(Todos.Where(t => t.CategoryId == categoryId));
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            var todo = Todos.FirstOrDefault(t => t.Id == id);
            return todo is not null ? Results.Ok(todo) : Results.NotFound();
        });

        group.MapPost("/", (CreateTodoRequest request) =>
        {
            var todo = new TodoItem(_nextId++, request.Title, request.CategoryId, request.IsComplete);
            Todos.Add(todo);
            return Results.Created($"/api/todos/{todo.Id}", todo);
        });

        group.MapPut("/{id:int}", (int id, UpdateTodoRequest request) =>
        {
            var index = Todos.FindIndex(t => t.Id == id);
            if (index == -1) return Results.NotFound();

            Todos[index] = Todos[index] with
            {
                Title = request.Title,
                CategoryId = request.CategoryId,
                IsComplete = request.IsComplete,
            };

            return Results.Ok(Todos[index]);
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            var index = Todos.FindIndex(t => t.Id == id);
            if (index == -1) return Results.NotFound();

            Todos.RemoveAt(index);
            return Results.NoContent();
        });
    }

    private record CreateTodoRequest(string Title, int CategoryId, bool IsComplete);
    private record UpdateTodoRequest(string Title, int CategoryId, bool IsComplete);
}
