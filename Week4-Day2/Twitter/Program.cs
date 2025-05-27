// You have been hired to build the backend for a Twitter-like application using .NET 8, Entity Framework Core (EF Core) 
// with PostgreSQL as the database. 
// The application supports basic social media features such as 
// user registration,
// posting tweets,
// liking tweets, 
// using hashtags, 
// and following users.
// Your goal is to model and implement the database layer only using EF Core with code-first approach, 
// focusing on data design, relationships,  migrations, and PostgreSQL-specific features.
using Twitter.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<TwitterContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();