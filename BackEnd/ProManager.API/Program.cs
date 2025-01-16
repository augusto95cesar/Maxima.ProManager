using Npgsql;
using ProManager.Infrastructure.Data.Repositories.Interface;
using ProManager.Infrastructure.Data.Repositories;
using System.Data;
using ProManager.Application.Interfaces;
using ProManager.Application.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;

#region Create Builder Services & Config de Services
var builder = WebApplication.CreateBuilder(args);

//injeção dependência do postgress
builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

//injeção dependência Repositorio
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

//injeção dependência Serviços
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configuração do esquema de segurança
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu_token}"
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
            new string[] {}
        }
    });

    // Filtro para autenticação por classe
    //c.OperationFilter<AuthOperationFilter>();

});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Token inválido: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"Token válido: {context.SecurityToken}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"Falha de autenticação: {context.ErrorDescription}");
                return Task.CompletedTask;
            }
        };
    });

// Carrega a lista de origens permitidas do appsettings.json
var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

// Configure a política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaDeCors", policy =>
    {
        policy.WithOrigins(allowedOrigins) // Domínios permitidos
              .AllowAnyHeader()  // Permite qualquer cabeçalho
              .AllowAnyMethod(); // Permite qualquer método (GET, POST, etc.)
    });
});
#endregion

#region App Build & Config App
var app = builder.Build(); // Build App
app.UseCors("PoliticaDeCors"); // Habilitar Politica de Cors 
// Configurar o Swagger para produção também
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();// Habilitar o Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Maxima.ProManager.API");
        c.RoutePrefix = string.Empty; // Para acessar diretamente no root
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Middleware para autenticação
app.UseAuthorization();  // Middleware para autorização 
app.MapControllers();
app.Run();
#endregion