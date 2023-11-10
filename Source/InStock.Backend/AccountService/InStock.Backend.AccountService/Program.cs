using InStock.Common.AccountService.Abstraction.Repositories;
using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Backend.AccountService.Core.Services;
using Refit;
using InStock.Common.Abstraction.Logger;
using ILogger = InStock.Common.Abstraction.Logger.ILogger;
using InStock.Backend.AccountService.Data.Repositories.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddRefitClient<IIdentityService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(InStock.Common.IdentityService.Abstraction.Constants.BaseUrl));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<ILogger, LocalSessionLogger>()
    .AddSingleton<IAccountRepository, DBAccountRepository>()
    .AddSingleton<IAccountService, AccountService>();

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

