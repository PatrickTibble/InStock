using InStock.Backend.IdentityService.Abstraction.Repositories;
using InStock.Backend.IdentityService.Abstraction.Services;
using InStock.Backend.IdentityService.Core.Services.Communication;
using InStock.Backend.IdentityService.Core.Services.Identity;
using InStock.Backend.IdentityService.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSingleton<ICommunicationService, CommunicationService>()
    .AddSingleton<IIdentityRepository, IdentityRepository>()
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
