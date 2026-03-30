namespace VerticalSliceApi.Features.Products;

public class ProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product(1, "Wireless Headphones", 79.99m, "Electronics"),
        new Product(2, "Domain-Driven Design", 44.95m, "Books"),
        new Product(3, "Mechanical Keyboard", 129.00m, "Electronics"),
        new Product(4, "The Pragmatic Programmer", 39.99m, "Books"),
    };

    private int _nextId = 5;

    public IReadOnlyList<Product> GetAll() => _products.AsReadOnly();

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public Product Add(string name, decimal price, string category)
    {
        var product = new Product(_nextId++, name, price, category);
        _products.Add(product);
        return product;
    }

    public bool Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null) return false;
        _products.Remove(product);
        return true;
    }
}
