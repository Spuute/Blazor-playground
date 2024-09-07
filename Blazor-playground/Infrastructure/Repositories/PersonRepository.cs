using Application.Abstractions.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonRepository(ApplicationDbContext context) : IPersonRepository
{
    
    public async Task<Person?> GetById(int id)
    {
        return await context.Persons!.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Person>> GetAll()
    {
        return await context.Persons!.ToListAsync();
    }

    public async Task<Person> Add(Person person)
    {
        context.Persons!.Add(person);
        await context.SaveChangesAsync();
        return person;
    }

    public async Task<Person> Update(Person person)
    {
        context.Persons!.Update(person);
        await context.SaveChangesAsync();
        return person;
    }

    public async Task<Person> Delete(Person person)
    {
        context.Remove(person);
        await context.SaveChangesAsync();
        return person;
    }
}