using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VersiyonTakip.Models;
using VersiyonTakip.Services;

namespace VersiyonTakip.Pages;

public class IndexModel : PageModel
{
    private readonly GitHubReleaseService _releaseService;

    public List<GitHubReleases> Releases { get; private set; } = new();

    public GitHubReleases? LatestRelease { get; private set; }

    public string? ErrorMessage { get; private set; }

    public IndexModel(GitHubReleaseService releaseService)
    {
        _releaseService = releaseService;
    }

    public async Task OnGetAsync()
    {
        try
        {
            // 1. GitHub'dan sürümleri çek
            Releases = await _releaseService.GetReleasesAsync();

            // 2. Sürümleri yayın tarihine göre sırala
            Releases = Releases
                .OrderByDescending(r =>
                    r.PublishedAt ?? DateTimeOffset.MinValue)
                .ToList();

            // 3. En son sürümü belirle
            LatestRelease = Releases.FirstOrDefault();
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