using Npgsql;
using ProManager.Infrastructure.Data.Repositories.Interface;
using ProManager.Infrastructure.Data.Repositories;
using System.Data;
using ProManager.Application.Interfaces;
using ProManager.Application.Services;

var builder = WebApplication.CreateBuilder(args);


//injeção dependência do postgress
builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

//injeção dependência repositorio
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

//injeção dependência repositorio
builder.Services.AddScoped<IProdutoService, ProdutoService>();


// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
