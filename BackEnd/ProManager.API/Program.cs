using Npgsql;
using ProManager.Infrastructure.Data.Repositories.Interface;
using ProManager.Infrastructure.Data.Repositories;
using System.Data;
using ProManager.Application.Interfaces;
using ProManager.Application.Services;

var builder = WebApplication.CreateBuilder(args);


//inje��o depend�ncia do postgress
builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

//inje��o depend�ncia repositorio
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

//inje��o depend�ncia repositorio
builder.Services.AddScoped<IProdutoService, ProdutoService>();


// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
