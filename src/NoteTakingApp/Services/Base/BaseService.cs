using Polly;
using System;
using System.Threading.Tasks;

namespace NoteTakingApp.Services
{
    public class BaseService
    {
        protected async Task<PolicyResult<T>> InvokeWithPolicyAsync<T>(Func<Task<T>> task)
        {
            return await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAndCaptureAsync(task);
        }
    }
}
