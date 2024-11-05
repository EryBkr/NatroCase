using Application.Services.RdapServices;
using Core.Polly.Policy;
using Domain.Dtos;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Services.Rdap;

public sealed class RdapService : IRdapService
{
    private readonly HttpClient _httpClient;
    private readonly PolicyProvider _policyProvider;

    public RdapService(HttpClient httpClient, PolicyProvider policyProvider)
    {
        _httpClient = httpClient;
        _policyProvider = policyProvider;
    }

    public async Task<RdapResponse> CheckDomainAvailabilityAsync(string domain)
    {
        var retryPolicy = _policyProvider.GetRetryPolicy();

        var response = await retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(domain));

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<RdapResponse>(content);
    }
}
