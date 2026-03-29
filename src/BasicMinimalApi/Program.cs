using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/login", () => Results.Content("""
     <html>
     <body>
         <form method="post" action="/login">
             <input name="username" placeholder="Username" />
             <input name="password" type="password" />
             <button type="submit">Login</button>
         </form>
     </body>
     </html>
 """, "text/html"));

// If using LoginRequest, the incoming data must be JSON. For form data,
// we need to read it manually from the request. Using the context allows
// us to handle both JSON and form submissions. You probably wouldn't
// do this in a real app, but this is just meant as a sample.
app.MapPost("/login", async (HttpContext ctx) =>
{
    string username;
    string password;

    if (ctx.Request.HasJsonContentType())
    {
        var body = await ctx.Request.ReadFromJsonAsync<LoginRequest>();
        username = body!.Username;
        password = body!.Password;
    }
    else
    {
        var form = await ctx.Request.ReadFormAsync();
        username = form["username"].ToString();
        password = form["password"].ToString();
    }

    if (username == "admin" && password == "password")
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("department", "Engineering")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return Results.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    return Results.Json(new { error = "Invalid credentials" }, statusCode: 401);
});

app.Run();
app.UseCors();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


record LoginRequest(string Username, string Password);