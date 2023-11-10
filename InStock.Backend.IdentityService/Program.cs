using InStock.Common.IdentityService.Abstraction.Repositories;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Data.Repositories;
using InStock.Backend.IdentityService.Core.Services;
using InStock.Common.Abstraction.Logger;
using ILogger = InStock.Common.Abstraction.Logger.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSingleton<ITokenService, JwtSecurityTokenService>()
    .AddSingleton<ILogger, LocalSessionLogger>()
    .AddSingleton<IIdentityService, IdentityService>()
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    
    .AddControllers();

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
