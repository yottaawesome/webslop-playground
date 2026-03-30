namespace VerticalSliceApi.Features.Products;

public static class ListProducts
{
    public static IResult Handle(ProductRepository repo)
    {
        return Results.Ok(repo.GetAll());
    }
}
