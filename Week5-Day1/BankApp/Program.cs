using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Mappers;
using BankApp.Repositories;
using BankApp.SearchFunctions;
using BankApp.Services;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);


// 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

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
builder.Services.AddTransient<IFaqService, FaqService>();


builder.Services.AddTransient<IFaqRepository>(provider =>
    new CsvFaqRepository("BankFAQs.csv"));

builder.Services.AddHttpClient();

builder.Services.AddTransient<TransactionMapper>();


builder.Services.AddDbContext<BankContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
