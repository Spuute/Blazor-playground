using BlazorPlaygroundWeb.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorPlaygroundWeb.Pages;

public partial class Person
{
    [Inject] private IHttpClientService HttpClientService { get; set; }
    [Parameter] public int Id { get; set; }

    private DTO.Person _person;
    private bool _isLoading = true;
    private Exception _loadFailed;
    
    protected override async Task OnInitializedAsync()
    {
        await FetchPersonById();
    }

    private async Task FetchPersonById()
    {
        try
        {
            _isLoading = true;
            _person = await HttpClientService.Get<DTO.Person>($"/api/person/{Id}");
        }
        catch (Exception e)
        {
            _loadFailed = e;
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _isLoading = false;
        }
    }
}