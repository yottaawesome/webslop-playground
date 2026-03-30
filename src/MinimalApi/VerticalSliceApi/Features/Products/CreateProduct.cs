namespace VerticalSliceApi.Features.Products;

public static class CreateProduct
{
    public record Request(string Name, decimal Price, string Category);

    public static IResult Handle(Request request, ProductRepository repo)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Results.BadRequest(new { error = "Name is required" });
        if (request.Price <= 0)
            return Results.BadRequest(new { error = "Price must be positive" });
        if (string.IsNullOrWhiteSpace(request.Category))
            return Results.BadRequest(new { error = "Category is required" });

        var product = repo.Add(request.Name, request.Price, request.Category);
        return Results.Created($"/api/products/{product.Id}", product);
    }
}
