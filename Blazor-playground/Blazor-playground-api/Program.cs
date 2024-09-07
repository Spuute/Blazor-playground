using Application;
using Application.Abstractions.Interfaces;
using Domain;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/person", async (Person person, IPersonRepository personRepository) =>
{
    if (person is not null)
        await personRepository.Add(person);

    return Results.Created($"/person/{person!.Id}", person);
});

app.MapGet("/person/{id}:int", async (int id, IPersonRepository personRepository) =>
{
    var person = await personRepository.GetById(id);

    return person is not null ? Results.Ok(person) : Results.NotFound();
});

app.MapGet("/persons", async (IPersonRepository personRepository) =>
{
    var personList = await personRepository.GetAll();

    return personList is not null ? Results.Ok(personList) : Results.NotFound();
});

app.Run();