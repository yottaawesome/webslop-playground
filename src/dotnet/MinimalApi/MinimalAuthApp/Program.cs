using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
var jwtAudience = builder.Configuration["Jwt:Audience"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateLifetime = true
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

// Serve login page (public)
app.MapGet("/login", (IWebHostEnvironment env) =>
    Results.File(Path.Combine(env.WebRootPath, "login.html"), "text/html"));

// Serve home page shell (public — JS handles auth)
app.MapGet("/", (IWebHostEnvironment env) =>
    Results.File(Path.Combine(env.WebRootPath, "index.html"), "text/html"));

// Issue a JWT token
app.MapPost("/api/login", (LoginRequest request) =>
{
    // Demo credentials — replace with real validation
    if (request.Username == "admin" && request.Password == "password")
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
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

// Protected endpoint — returns the user's claims
app.MapGet("/api/me", (ClaimsPrincipal user) =>
{
    var name = user.FindFirst(ClaimTypes.Name)?.Value;
    var role = user.FindFirst(ClaimTypes.Role)?.Value;
    var dept = user.FindFirst("department")?.Value;

    return Results.Ok(new { name, role, department = dept });
}).RequireAuthorization();

// Protected endpoint requiring Admin role
app.MapGet("/api/admin", (ClaimsPrincipal user) =>
{
    var name = user.FindFirst(ClaimTypes.Name)?.Value;
    return Results.Ok(new { message = $"Hello admin {name}!", access = "granted" });
}).RequireAuthorization(policy => policy.RequireRole("Admin"));

app.Run();

record LoginRequest(string Username, string Password);
