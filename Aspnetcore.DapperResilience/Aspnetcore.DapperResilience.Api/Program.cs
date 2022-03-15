using Aspnetcore.DapperResilience.Domain.Entities;
using Aspnetcore.DapperResilience.Domain.Repositories;
using Aspnetcore.DapperResilience.Infra.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddDependencyInjection()
                .AddDapperClient(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



#region endpoints

app.MapGet("/address", async ([FromServices] IAdressRepository repository) =>

await repository.GetAddressesAsync()
                is IEnumerable<Address> addresses
                ? Results.Ok(addresses)
                : Results.NotFound()
                );


app.MapGet("/address/{id}", async (Guid id, [FromServices] IAdressRepository repository) =>
    await repository.GetAddressesAsync(id)
        is Address address
            ? Results.Ok(address)
            : Results.NotFound()
          );

app.MapPost("/address", async (Address address, [FromServices] IAdressRepository repository) =>
{
    await repository.SaveAddressAsync(address);

    return Results.Created($"/address/{address.Id}", address);
});

#endregion

app.Run();
