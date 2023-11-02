using InStock.Backend.AccountService.Abstraction.Services;
using InStock.Backend.AccountService.Core.Services.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#if DEBUGWITHMOCKS

builder.Services.AddSingleton<IAccountService, MockAccountService>();

#else

builder.Services.AddSingleton<IAccountService, AccountService>();

#endif

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

