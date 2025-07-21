using MyApiApp.Interfaces;
using MyApiApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IWelcomeService, WelcomeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80); // ✅ Important
});


var app = builder.Build();
Console.WriteLine("API is starting...");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();