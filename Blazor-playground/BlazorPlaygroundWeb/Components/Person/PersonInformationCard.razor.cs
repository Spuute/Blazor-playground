using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorPlaygroundWeb.Components.Person;

public partial class PersonInformationCard
{
    [Inject] private IDialogService DialogService { get; set; }
    [Parameter] public DTO.Person PersonObject { get; set; }
    [CascadingParameter] public EventCallback ReFetchDataCallback { get; set; }

    private async Task RefetchData()
    {
        if (ReFetchDataCallback.HasDelegate)
            await ReFetchDataCallback.InvokeAsync();
    }
    
    private async Task OpenEditDialog()
    {
        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Medium,
            
        };
        var parameters = new DialogParameters<EditPersonDialog>
        {
            { x => x.PersonObject, PersonObject }
        };
        var dialog = DialogService.Show<EditPersonDialog>("Edit person", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            
        }
    }
}