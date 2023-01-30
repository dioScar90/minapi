using minapi.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service to the container.
builder.Services.AddSingleton<DevEventsDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//     app.UseDeveloperExceptionPage();

// app.UseSwagger();
// // app.MapGet("/cliente", () => new Cliente("Diogo", "diogols@live.com"));
// app.UseSwaggerUI();


// public class Cliente
// {
//     public Guid Id { get; set; }
//     public string? Nome { get; set; }
// }
// public class AppDbContext : DbContext
// {
//     public AppDbContext(DbContextOptions options) : base(options) {}
//     public DbSet<Cliente> Clientes { get; set; }
// }
