using BlazorPlaygroundWeb.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorPlaygroundWeb.Pages;

public partial class Persons
{
    [Inject] private IHttpClientService HttpClientService { get; set; }
    [Inject] private NavigationManager NavManager { get; set; }
    private string? Search { get; set; }
    private List<DTO.Person> _persons = new();
    
    protected override async Task OnInitializedAsync()
    {
        await FetchPersons();
    }

    private void GoTo(TableRowClickEventArgs<DTO.Person> selectedPerson)
    {
        NavManager.NavigateTo($"/person/{selectedPerson.Item!.Id}");
    }

    private async Task FetchPersons()
    {
        try
        {
            _persons = await HttpClientService.Get<List<DTO.Person>>($"api/persons");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}