using GestionContactos.Application.Services;
using GestionContactos.Infrastructure.Context;
using GestionContactos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Configuraci�n de la base de datos
builder.Services.AddDbContext<GestionContactosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyecci�n de dependencias
builder.Services.AddScoped<DatoUsuarioRepository>();
builder.Services.AddScoped<DatoUsuarioService>();

// Configuraci�n de Swagger
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configuraci�n de middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());


app.UseAuthorization();
app.MapControllers();

app.Run();