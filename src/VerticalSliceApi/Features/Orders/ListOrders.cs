namespace VerticalSliceApi.Features.Orders;

public static class ListOrders
{
    public static IResult Handle(OrderRepository repo)
    {
        return Results.Ok(repo.GetAll());
    }
}
