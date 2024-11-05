using Polly;
using Polly.Extensions.Http;

namespace Core.Polly.Policy;

public class PolicyProvider
{
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(r => r.StatusCode == System.Net.HttpStatusCode.InternalServerError)
              .RetryAsync(3);
    }
}

