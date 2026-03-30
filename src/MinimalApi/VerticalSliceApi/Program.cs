using VerticalSliceApi.Features.Products;
using VerticalSliceApi.Features.Orders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<OrderRepository>();

var app = builder.Build();

app.MapProductEndpoints();
app.MapOrderEndpoints();

app.Run();
