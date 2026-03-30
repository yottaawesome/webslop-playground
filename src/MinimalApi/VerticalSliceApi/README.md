# VerticalSliceApi — Vertical Slice Architecture for Minimal APIs

A sample ASP.NET Minimal API project demonstrating **vertical slice architecture** for organizing a larger codebase. This e-commerce API manages Products and Orders using in-memory data stores, and is designed to show how feature-based folder structure scales better than a single `Program.cs` file.

## Why Vertical Slices?

As a Minimal API grows beyond a handful of endpoints, keeping everything in one file becomes unmanageable. The traditional layered approach (controllers, services, repositories in separate folders) scatters a single feature across the entire project. Vertical slices solve this by **grouping all code for a feature together**:

- **Feature isolation** — everything for Products lives in `Features/Products/`. You never hunt across layers.
- **Self-contained slices** — each handler file contains its request model, validation, and logic. You can understand a feature by reading one folder.
- **Independent evolution** — changing the Orders feature doesn't touch any Products code. Teams can work in parallel without merge conflicts.
- **Easy navigation** — need to debug "create product"? Open `Features/Products/CreateProduct.cs`. Done.

## How It Compares to Extension Methods

For small projects (5–10 endpoints), a simple extension-method approach that maps routes in `Program.cs` is perfectly fine. Vertical slices become valuable when you have multiple features with distinct models, validation, and business logic. This project shows the pattern so you can adopt it when complexity warrants it.

## Key Principles

| Principle | Implementation |
|---|---|
| One handler per file | `CreateProduct.cs`, `ListOrders.cs`, etc. |
| Feature folders | `Features/Products/`, `Features/Orders/` |
| Shared models within a feature | `ProductModels.cs`, `OrderModels.cs` |
| Cross-cutting concerns in Common | `Common/Filters/RequestLoggingFilter.cs` |
| DI for stateful services | Repositories registered as singletons |

## Project Structure

```
VerticalSliceApi/
├── Program.cs                         # Composition root
├── Features/
│   ├── Products/
│   │   ├── ProductEndpoints.cs        # Route group mapping
│   │   ├── ListProducts.cs            # GET /api/products
│   │   ├── GetProduct.cs              # GET /api/products/{id}
│   │   ├── CreateProduct.cs           # POST /api/products
│   │   ├── DeleteProduct.cs           # DELETE /api/products/{id}
│   │   ├── ProductModels.cs           # Shared record types
│   │   └── ProductRepository.cs       # In-memory data store
│   └── Orders/
│       ├── OrderEndpoints.cs
│       ├── ListOrders.cs              # GET /api/orders
│       ├── GetOrder.cs                # GET /api/orders/{id}
│       ├── CreateOrder.cs             # POST /api/orders
│       ├── OrderModels.cs
│       └── OrderRepository.cs
└── Common/
    └── Filters/
        └── RequestLoggingFilter.cs    # Endpoint filter for timing/logging
```

## Running

```bash
cd src/VerticalSliceApi
dotnet run
```

The API starts on `http://localhost:5000` by default (check console output for the actual port).

## Testing with curl

```bash
# List all products
curl http://localhost:5000/api/products

# Get a single product
curl http://localhost:5000/api/products/1

# Create a product
curl -X POST http://localhost:5000/api/products \
  -H "Content-Type: application/json" \
  -d '{"name":"USB-C Hub","price":34.99,"category":"Electronics"}'

# Delete a product
curl -X DELETE http://localhost:5000/api/products/2

# Create an order (calculates total from product prices)
curl -X POST http://localhost:5000/api/orders \
  -H "Content-Type: application/json" \
  -d '{"productIds":[1,3]}'

# List all orders
curl http://localhost:5000/api/orders
```
