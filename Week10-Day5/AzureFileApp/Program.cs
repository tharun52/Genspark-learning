using AzureFileApp.Interface;
using BlobAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBlobStorageService, BlobStorageService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                       
    app.UseSwaggerUI();              
     app.MapOpenApi();
}

app.UseHttpsRedirection();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
