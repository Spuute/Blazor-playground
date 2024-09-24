using Application;
using Application.Abstractions.Interfaces;
using Domain;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapPost("/api/person", async (Person person, IPersonRepository personRepository) =>
{
    if (person is not null)
        await personRepository.Add(person);

    return Results.Created($"/api/person/{person!.Id}", person);
});

app.MapGet("/api/person/{id:int}", async (int id, IPersonRepository personRepository) =>
{
    var person = await personRepository.GetById(id);

    return person is not null ? Results.Ok(person) : Results.NotFound();
});

app.MapGet("/api/persons", async (IPersonRepository personRepository) =>
{
    var personList = await personRepository.GetAll();

    return personList is not null ? Results.Ok(personList) : Results.NotFound();
});

app.MapPut("/api/persons", async (IPersonRepository personRepository, Person updatedPerson) =>
{
    
    var data = await personRepository.Update(updatedPerson);
    return data;
});

app.Run();