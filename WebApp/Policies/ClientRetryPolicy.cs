using Polly;
using Polly.Retry;

namespace WebApp.Policies;

public class ClientRetryPolicy
{
    public AsyncRetryPolicy<HttpResponseMessage> JustHttpRetry { get; set; }
    public AsyncRetryPolicy<HttpResponseMessage> HttpRetryWithWaiting { get; set; }
    public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; set; }

    public ClientRetryPolicy()
    {
        JustHttpRetry = Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
                              .RetryAsync(3);

        HttpRetryWithWaiting = Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
                                     .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(5));

        ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
                                     .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));
    }
}