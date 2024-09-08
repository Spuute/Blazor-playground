using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace BlazorPlaygroundWeb.Services;

public class HttpClientService(HttpClient httpClient) : IHttpClientService
{
    public async Task<T> Get<T>(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var result = await httpClient.SendAsync(request);

        if (result.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden or HttpStatusCode.Redirect)
        {
            throw new UnauthorizedAccessException();
        }

        var returnData = JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (returnData is null)
        {
            throw new InvalidDataException();
        }

        return returnData;
    }

    public async Task<TOutput> Put<TInput, TOutput>(string url, TInput postData)
    {
        var json = JsonSerializer.Serialize(postData);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await httpClient.PutAsync(url, httpContent);
        
        if (result.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden or HttpStatusCode.Redirect)
        {
            throw new UnauthorizedAccessException();
        }

        if (!result.IsSuccessStatusCode)
        {
            var message = await result.Content.ReadAsStringAsync();
            throw new HttpRequestException(message, null, result.StatusCode);
        }

        var returnData = JsonSerializer.Deserialize<TOutput>(await result.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (returnData is null)
        {
            throw new InvalidDataException();
        }

        return returnData;
    }

    public async Task<TOutput> Post<TInput, TOutput>(string url, TInput postData)
    {
        var json = JsonSerializer.Serialize(postData);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await httpClient.PostAsync(url, httpContent);

        if (result.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden or HttpStatusCode.Redirect)
        {
            throw new UnauthorizedAccessException();
        }
        
        if (!result.IsSuccessStatusCode)
        {
            var message = await result.Content.ReadAsStringAsync();
            throw new HttpRequestException(message, null, result.StatusCode);
        }

        var returnData = JsonSerializer.Deserialize<TOutput>(await result.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (returnData is null)
        {
            throw new InvalidDataException();
        }

        return returnData;
    }

    public async Task<T> Delete<T>(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        var result = await httpClient.SendAsync(request);
        
        if (result.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden or HttpStatusCode.Redirect)
        {
            throw new UnauthorizedAccessException();
        }

        if (!result.IsSuccessStatusCode)
        {
            var message = await result.Content.ReadAsStringAsync();
            throw new HttpRequestException(message, null, result.StatusCode);
        }

        var returnData = JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (returnData is null)
        {
            throw new InvalidDataException();
        }

        return returnData;
    }
}