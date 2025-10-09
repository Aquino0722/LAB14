using LabLINQ.Models;
using LabLINQ.Repositories;
using LabLINQ.Repositories.Interfaces;
using LabLINQ.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar servicios
// --- Conexión a la base de datos MySQL (leída desde appsettings.json) ---
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(Program));

// --- Repositorios y Unit of Work ---
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// --- Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "LabLINQ API", Version = "v1" });
});


var app = builder.Build();

// 2. Configurar el pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();