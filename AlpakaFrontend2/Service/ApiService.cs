using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RestAlpaka.Model; 


namespace AlpakaFrontend2.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient) 
        {  
            this._httpClient = httpClient; 
        }


        public async Task CreateEvent(Event @event)
        {
            await _httpClient.PostAsJsonAsync("Event", @event);
         
        }

        public async Task<T> GetTAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<T>(endpoint); 
        }

        public async Task CreateAsync<T>(string endpoint, T data)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, data);
        }


         public async Task<TResponse> CreateAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync<TRequest>(endpoint, data);
            // Assuming you want to return some response data, adjust as needed
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

    }
}
