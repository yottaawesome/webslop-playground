namespace VerticalSliceApi.Features.Orders;

public class OrderRepository
{
    private readonly List<Order> _orders = new()
    {
        new Order(1, new List<int> { 1, 2 }, 124.94m, new DateTime(2025, 1, 15, 10, 30, 0, DateTimeKind.Utc)),
        new Order(2, new List<int> { 3 }, 129.00m, new DateTime(2025, 1, 16, 14, 0, 0, DateTimeKind.Utc)),
    };

    private int _nextId = 3;

    public IReadOnlyList<Order> GetAll() => _orders.AsReadOnly();

    public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public Order Add(List<int> productIds, decimal total)
    {
        var order = new Order(_nextId++, productIds, total, DateTime.UtcNow);
        _orders.Add(order);
        return order;
    }
}
