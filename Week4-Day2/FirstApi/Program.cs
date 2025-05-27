using FirstApi.Repositories;
using FirstApi.Services;
using FirstApi.Interfaces;
using FirstApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// builder.Services.AddScoped<IRepository<int, Doctor>, DoctorRepository>();
// builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddSingleton<IRepository<int, Doctor>, DoctorRepository>();
builder.Services.AddSingleton<IDoctorService, DoctorService>();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.MapControllers();
app.Run();
