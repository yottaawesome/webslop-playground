namespace VerticalSliceApi.Features.Orders;

public record Order(int Id, List<int> ProductIds, decimal Total, DateTime CreatedAt);
