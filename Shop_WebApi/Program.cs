using Microsoft.EntityFrameworkCore;
using Shop_WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ShopDbContext>(option => option.UseSqlServer("Server=DESKTOP-HJIL55T\\SQLEXPRESS;Database=shop_db;TrustServerCertificate=true;Trusted_Connection=true"));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
