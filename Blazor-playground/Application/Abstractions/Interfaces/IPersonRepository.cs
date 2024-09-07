using Domain.Entities;

namespace Application.Abstractions.Interfaces;

public interface IPersonRepository
{
    Task<Person?> GetById(int id);
    Task<List<Person>> GetAll();
    Task<Person> Add(Person person);
    Task<Person> Update(Person person);
    Task<Person> Delete(Person person);
}