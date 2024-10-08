using BlazorPlaygroundWeb.Components;
using BlazorPlaygroundWeb.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorPlaygroundWeb.Pages;

public partial class Person
{
    [Inject] private IHttpClientService HttpClientService { get; set; }
    [Parameter] public int Id { get; set; }

    private static DTO.Person? _person;
    private bool _isLoading = true;
    private Exception? _loadFailed;
    
    protected override async Task OnInitializedAsync()
    {
        await FetchPersonById();
    }

    private async Task FetchPersonById()
    {
        try
        {
            // _person = null;
            _isLoading = true;
            _person = await HttpClientService.Get<DTO.Person>($"/api/person/{Id}");
        }
        catch (Exception? e)
        {
            _loadFailed = e;
            Console.WriteLine(e);
        }
        finally
        {
            _isLoading = false;
        }
    }
    private EventCallback ReFetchDataCallback => EventCallback.Factory.Create(this, RefetchData);

    private async Task RefetchData()
    {
        _isLoading = true;
        await FetchPersonById();
        _isLoading = false;
        StateHasChanged();
    }

   
}