using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VideoManager.Data;
using VideoManager.Interfaces;
using VideoManager.Models;
using VideoManager.Repositories;
using VideoManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRepository<Guid, TrainingVideo>, TrainingVideoRepository>();

builder.Services.AddTransient<IFileService, FileService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
