using System.Net.Http;
using System.Threading.Tasks;
using ApiLand.Interfaces;

public class ApiService : IApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiService( IHttpClientFactory httpClientFactory )
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> FetchDataAsync( string apiType )
    {
        var client = _httpClientFactory.CreateClient();

        // Determine the API endpoint based on the selection
        string apiEndpoint = apiType switch
        {
            "api1" => "https://api.example.com/endpoint1",
            "api2" => "https://api.example.com/endpoint2",
            _ => throw new ArgumentException( "Invalid API type", nameof( apiType ) )
        };

        var response = await client.GetAsync( apiEndpoint );

        if ( !response.IsSuccessStatusCode )
        {
            throw new HttpRequestException( $"API call failed with status code: {response.StatusCode}" );
        }

        return await response.Content.ReadAsStringAsync();
    }
}
