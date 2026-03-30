# SmallApi-ExtensionMethods

A minimal ASP.NET Minimal API sample that uses **extension methods** to organize endpoints. This pattern keeps `Program.cs` clean while grouping related routes into separate files — ideal for small projects with a handful of features.

## Why Extension Methods?

For small APIs (a few CRUD resources, no complex business logic), this pattern hits a sweet spot:

- **Flat hierarchy** — no deep folder nesting, just `Endpoints/` and `Models/`.
- **Easy to navigate** — each feature maps to one file. Open `TodoEndpoints.cs` and you see every todo route.
- **Minimal ceremony** — no interfaces, no DI registrations, no base classes. Static extension methods and static in-memory lists are all you need.
- **Clean Program.cs** — the entry point reads like a table of contents: `app.MapTodoEndpoints()`, `app.MapCategoryEndpoints()`.

Each `Endpoints/XxxEndpoints.cs` file exposes a single extension method on `WebApplication` that calls `MapGroup()` and defines all routes for that feature.

## When to Graduate

Consider moving to a **vertical-slice** architecture when features start needing their own models, services, validators, or middleware. If an endpoint file grows past ~150 lines or you find yourself wanting dependency injection and unit tests for business logic, it's time to restructure.

## Project Structure

```
SmallApi-ExtensionMethods/
├── Program.cs                        # Entry point — registers endpoints
├── Models/
│   ├── TodoItem.cs                   # Todo record type
│   └── Category.cs                   # Category record type
├── Endpoints/
│   ├── TodoEndpoints.cs              # GET/POST/PUT/DELETE for todos
│   └── CategoryEndpoints.cs          # GET/POST/DELETE for categories
├── SmallApi-ExtensionMethods.csproj
└── README.md
```

## Run

```bash
dotnet run
```

The API starts on `http://localhost:5000` (or the port shown in console output). Try:

```bash
curl http://localhost:5000/api/todos
curl http://localhost:5000/api/todos?categoryId=2
curl http://localhost:5000/api/categories
```
