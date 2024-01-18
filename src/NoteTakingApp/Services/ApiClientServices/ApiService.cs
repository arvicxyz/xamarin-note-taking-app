using NoteTakingApp.Constants;
using Refit;

namespace NoteTakingApp.Services.ApiClientServices
{
    public class ApiService<T> : IApiService<T>
    {
        public T Api { get; }

        public ApiService()
        {
            Api = RestService.For<T>(AppConstants.WebApiBaseUrl);
        }
    }
}
