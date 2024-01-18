namespace NoteTakingApp.Services.ApiClientServices
{
    public interface IApiService<T>
    {
        T Api { get; }
    }
}
