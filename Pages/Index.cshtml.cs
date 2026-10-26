using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using VersiyonTakip.Models;
using VersiyonTakip.Services;

namespace VersiyonTakip.Pages;

public class IndexModel : PageModel
{
    private readonly GitHubReleaseService _releaseService;

    public List<GitHubReleases> Releases { get; private set; } = new();

    public string? ErrorMessage { get; private set; }

    public IndexModel(GitHubReleaseService releaseService)
    {
        _releaseService = releaseService;
    }

    public async Task OnGetAsync()
    {
        try
        {
            Releases = await _releaseService.GetReleasesAsync();
        }
        catch (HttpRequestException)
        {
            ErrorMessage = "GitHub'a bağlanılamadı.";
        }
        catch (JsonException)
        {
            ErrorMessage = "GitHub'dan gelen veriler okunamadı.";
        }
    }
}