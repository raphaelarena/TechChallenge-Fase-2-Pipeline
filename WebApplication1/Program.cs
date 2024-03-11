using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication1.Interface;
using WebApplication1.Repository;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sua API", Version = "v1" });

    // Configuração para adicionar suporte a autenticação Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT nesta caixa. Exemplo: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
           Array.Empty<string>()
        }
    });
});

// Configure IUserRepository e ApplicationDbContext com o mesmo ciclo de vida
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Secret"));

builder.Services.AddAuthentication(
    x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
   ).AddJwtBearer(x =>
   {
       x.RequireHttpsMetadata = false;
       x.SaveToken = true;
       x.TokenValidationParameters = new TokenValidationParameters()
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(key),
           ValidateIssuer = false,
           ValidateAudience = false
       };
   });

var app = builder.Build();

// Middleware para redirecionar para HTTPS
app.UseHttpsRedirection();

// Middleware de Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para endpoints da API
app.MapControllers();

app.Run();
