namespace ApiLand.Interfaces
{
    public interface IApiService
    {
        Task<string> FetchDataAsync( string apiType );
    }
}