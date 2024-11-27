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
        string apiEndpoint = "";
        switch ( apiType )
        {
            case "openbrewery":
                apiEndpoint = "https://api.openbrewerydb.org/v1/breweries";
                break;
            case "randomdog":
                apiEndpoint = "https://random.dog/woof.json";
                break;
            default:
                break;
        }

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync( apiEndpoint );

        if ( !response.IsSuccessStatusCode )
        {
            throw new HttpRequestException( $"API call failed with status code: {response.StatusCode}" );
        }

        return await response.Content.ReadAsStringAsync();
    }
}
