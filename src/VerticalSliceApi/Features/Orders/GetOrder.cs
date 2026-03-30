namespace VerticalSliceApi.Features.Orders;

public static class GetOrder
{
    public static IResult Handle(int id, OrderRepository repo)
    {
        var order = repo.GetById(id);
        return order is not null
            ? Results.Ok(order)
            : Results.NotFound(new { error = $"Order {id} not found" });
    }
}
