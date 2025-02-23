using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinApiLib.DependencyInjection;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
var strKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidConfigurationException("Jwt:Key is missing in appsettings.json");
var key = Encoding.UTF8.GetBytes(strKey);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddShelterInMemoryDatabase();
}
else
{
    builder.Services.AddShelterDatabase();
}
builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddAssembly()
    .AddOpenApi(opt =>
    {
        opt.ShouldInclude = (type) => type.GroupName == "Animals";
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services
    .AddAuthorization()
    .AddShelterAuth(configureJwt: builder.Configuration.GetSection("Jwt").Bind);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTheme(ScalarTheme.BluePlanet)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithDarkMode(true);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapEndpoints();
app.MapControllers();

await app.RunAsync();
