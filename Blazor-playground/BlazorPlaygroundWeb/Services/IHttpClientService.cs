namespace BlazorPlaygroundWeb.Services;

public interface IHttpClientService
{
    Task<T> Get<T>(string url);
    Task<TOutput> Put<TInput, TOutput>(string url, TInput postData);
    Task<TOutput> Post<TInput, TOutput>(string url, TInput postData);
    Task<T> Delete<T>(string url);
}