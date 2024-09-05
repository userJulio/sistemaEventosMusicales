using EventosMusicales.Controllers;
using EventosMusicales.Persistence;
using EventosMusicales.Repositories;
using EventosMusicales.Services.Implemetation;
using EventosMusicales.Services.Interface;
using EventosMusicales.Services.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configruacion context and conection string
builder.Services.AddDbContext<AplicactionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Registrar Servicios del controlador
builder.Services.AddScoped<IGeneroRepositorio,GeneroRepositorio>();
builder.Services.AddScoped<IConciertoRepository,ConciertoRepository>();
builder.Services.AddScoped<IConciertoService, ConciertoService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
//Configurando el automapper  : Agrgar perfiles de mappeo
builder.Services.AddAutoMapper(configure =>
{
    configure.AddProfile<ConciertoProfile>();
    configure.AddProfile<GeneroProfile>();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
