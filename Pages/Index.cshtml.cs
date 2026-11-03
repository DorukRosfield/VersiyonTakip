using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Linq;
using VersiyonTakip.Models;
using VersiyonTakip.Services;

namespace VersiyonTakip.Pages;

public class IndexModel : PageModel
{
    private readonly GitHubReleaseService _releaseService;

    public List<GitHubReleases> Releases { get; set; } = new();

    public GitHubReleases? LatestRelease { get; set; }

    public string? ErrorMessage { get; set; }

    public IndexModel(GitHubReleaseService releaseService)
    {
        _releaseService = releaseService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        // Kullanıcı giriş yapmış mı kontrol et
        if (HttpContext.Session.GetString("UserName") == null)
        {
            return RedirectToPage("/Login");
        }

        try
        {
            Releases = await _releaseService.GetReleasesAsync();

            Releases = Releases
                .OrderByDescending(x => x.PublishedAt)
                .ToList();

            LatestRelease = Releases.FirstOrDefault();
        }
        catch (HttpRequestException)
        {
            ErrorMessage = "GitHub bağlantısı başarısız!";
        }
        catch (JsonException)
        {
            ErrorMessage = "GitHub verileri okunamadı!";
        }

        return Page();
    }
}