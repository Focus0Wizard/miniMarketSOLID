using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace miniMarketSolid.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        public void OnGet() { }
    }
}