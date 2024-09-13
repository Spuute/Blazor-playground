using Microsoft.AspNetCore.Components;

namespace BlazorPlaygroundWeb.Components.Person;

public partial class PersonInformationCard
{
    [Parameter] public string FirstName { get; set; }
    [Parameter] public string LastName { get; set; }
    [Parameter] public string JobTitle { get; set; }
    [CascadingParameter] public EventCallback ReFetchDataCallback { get; set; }

    private async Task RefetchData()
    {
        if (ReFetchDataCallback.HasDelegate)
            await ReFetchDataCallback.InvokeAsync();
    }
}