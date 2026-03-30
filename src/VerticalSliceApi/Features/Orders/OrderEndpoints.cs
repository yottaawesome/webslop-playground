using VerticalSliceApi.Common.Filters;

namespace VerticalSliceApi.Features.Orders;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/orders")
            .AddEndpointFilter<RequestLoggingFilter>();

        group.MapGet("/", ListOrders.Handle);
        group.MapGet("/{id:int}", GetOrder.Handle);
        group.MapPost("/", CreateOrder.Handle);
    }
}
