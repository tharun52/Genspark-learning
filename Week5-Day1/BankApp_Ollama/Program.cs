using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Mappers;
using BankApp.Repositories;
using BankApp.SearchFunctions;
using BankApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.WriteIndented = true;
                });

builder.Services.AddTransient<IAccountService, AccountServices>();
builder.Services.AddTransient<ISearchAccounts, SearchAccounts>();
builder.Services.AddTransient<ITransactionService, TransactionServices>();

builder.Services.AddTransient<TransactionMapper>();

var csvPath = Path.Combine(builder.Environment.ContentRootPath, "BankFAQ.csv");
builder.Services.AddSingleton<IFAQRepository>(new CsvFaqRepository(csvPath));

builder.Services.AddSingleton<IChatHistoryRepository, InMemoryChatHistoryRepository>();

builder.Services.AddDbContext<BankContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.UseHttpsRedirection();
app.Run();
