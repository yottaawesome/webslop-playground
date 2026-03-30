using VerticalSliceApi.Features.Products;

namespace VerticalSliceApi.Features.Orders;

public static class CreateOrder
{
    public record Request(List<int> ProductIds);

    public static IResult Handle(Request request, OrderRepository orderRepo, ProductRepository productRepo)
    {
        if (request.ProductIds is null || request.ProductIds.Count == 0)
            return Results.BadRequest(new { error = "At least one product ID is required" });

        var products = new List<Product>();
        foreach (var productId in request.ProductIds)
        {
            var product = productRepo.GetById(productId);
            if (product is null)
                return Results.BadRequest(new { error = $"Product {productId} not found" });
            products.Add(product);
        }

        var total = products.Sum(p => p.Price);
        var order = orderRepo.Add(request.ProductIds, total);
        return Results.Created($"/api/orders/{order.Id}", order);
    }
}
