namespace BlazorPlaygroundWeb.DTO;

public class Person
{
    public int Id { get; set; }
    public required string? FirstName { get; init; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }
}