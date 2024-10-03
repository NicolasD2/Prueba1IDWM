using apiPrueba.src.Data;
using apiPrueba.src.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar la cadena de conexión
string connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "Data Source=app.db";
builder.Services.AddDbContext<ApplicacionDBContext>(opt => opt.UseSqlite(connectionString)); // Cambiado a ApplicacionDBContext

var app = builder.Build();

// Inicializar la base de datos y agregar datos de prueba
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicacionDBContext>(); // Cambiado a ApplicacionDBContext
    DataSeeder.Initialize(services);
}

// Configuración para el desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
// Ejecutar la aplicación

app.Run();
