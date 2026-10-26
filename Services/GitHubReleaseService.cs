using System.Net.Http.Json;
using VersiyonTakip.Models;

namespace VersiyonTakip.Services;

public class GitHubReleaseService
{
    private readonly HttpClient _httpClient;

    public GitHubReleaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<GitHubReleases>> GetReleasesAsync()
    {
        using var response = await _httpClient.GetAsync("repos/DorukRosfield/VersiyonTakip/releases?per_page=100");
        response.EnsureSuccessStatusCode();

        return await response.Content.
        ReadFromJsonAsync<List<GitHubReleases>>() ?? new List<GitHubReleases>();

    }
}
