using VerticalSliceApi.Common.Filters;

namespace VerticalSliceApi.Features.Products;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products")
            .AddEndpointFilter<RequestLoggingFilter>();

        group.MapGet("/", ListProducts.Handle);
        group.MapGet("/{id:int}", GetProduct.Handle);
        group.MapPost("/", CreateProduct.Handle);
        group.MapDelete("/{id:int}", DeleteProduct.Handle);
    }
}
