using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace VersiyonTakip.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public string UserName { get; set; } = "";

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
            ErrorMessage = "Lütfen kullanıcı adınızı girin.";
            return Page();
        }

        HttpContext.Session.SetString("UserName", UserName);

        return RedirectToPage("/Index");
    }
}