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

//inje��o depend�ncia do postgress
builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

//inje��o depend�ncia Repositorio
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

//inje��o depend�ncia Servi�os
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddControllers();

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configura��o do esquema de seguran�a
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

    // Filtro para autentica��o por classe
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
                Console.WriteLine($"Token inv�lido: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"Token v�lido: {context.SecurityToken}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"Falha de autentica��o: {context.ErrorDescription}");
                return Task.CompletedTask;
            }
        };
    });

// Carrega a lista de origens permitidas do appsettings.json
var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

// Configure a pol�tica de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaDeCors", policy =>
    {
        policy.WithOrigins(allowedOrigins) // Dom�nios permitidos
              .AllowAnyHeader()  // Permite qualquer cabe�alho
              .AllowAnyMethod(); // Permite qualquer m�todo (GET, POST, etc.)
    });
});
#endregion

#region App Build & Config App
var app = builder.Build(); // Build App
app.UseCors("PoliticaDeCors"); // Habilitar Politica de Cors 
// Configurar o Swagger para produ��o tamb�m
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
app.UseAuthentication(); // Middleware para autentica��o
app.UseAuthorization();  // Middleware para autoriza��o 
app.MapControllers();
app.Run();
#endregion