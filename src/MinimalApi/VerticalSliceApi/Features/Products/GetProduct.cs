namespace VerticalSliceApi.Features.Products;

public static class GetProduct
{
    public static IResult Handle(int id, ProductRepository repo)
    {
        var product = repo.GetById(id);
        return product is not null
            ? Results.Ok(product)
            : Results.NotFound(new { error = $"Product {id} not found" });
    }
}
