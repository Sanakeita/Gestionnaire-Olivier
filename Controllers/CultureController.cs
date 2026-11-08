using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace GestionOliviers.Controllers;

public class CultureController : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetLanguage(string culture, string? returnUrl)
    {
        var supportedCultures = new[] { "fr", "en", "ar", "zh-CN" };
        if (!supportedCultures.Contains(culture, StringComparer.OrdinalIgnoreCase))
        {
            return BadRequest("Unsupported language.");
        }

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true });

        return Url.IsLocalUrl(returnUrl)
            ? LocalRedirect(returnUrl!)
            : RedirectToAction("Index", "Home");
    }
}
