namespace VerticalSliceApi.Features.Products;

public static class DeleteProduct
{
    public static IResult Handle(int id, ProductRepository repo)
    {
        return repo.Delete(id)
            ? Results.NoContent()
            : Results.NotFound(new { error = $"Product {id} not found" });
    }
}
