using ECommerceAPI.Application.Features.Auth.Command.Register;
using ECommerceAPI.Application.Features.Auth.Rules;
using ECommerceAPI.Application.Interfaces.Tokens;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Infrastructure.Persistence;
using ECommerceAPI.Infrastructure.Tokens;
using ECommerceAPI.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ------------------- DbContext -------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ------------------- Identity -------------------
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// ------------------- CORS -------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ------------------- Scoped Services -------------------
builder.Services.AddScoped<AuthRules>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

// ------------------- AutoMapper -------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// ------------------- MediatR -------------------
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(RegisterCommandHandler).Assembly);
});

// ------------------- JWT Authentication -------------------
var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();
var key = Encoding.UTF8.GetBytes(tokenSettings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        NameClaimType = "sub"
    };
});

// ------------------- Controllers & JSON -------------------
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    );

// ------------------- Swagger -------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// ------------------- Swagger UI -------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ------------------- Middleware -------------------
app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();  

app.MapControllers();

app.Run();
