using Danut_Sara_Lab2.Data;
using Danut_Sara_Lab2.Migrations;
using Danut_Sara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Danut_Sara_Lab2.Pages.AuthorsPages;

public class IndexModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public IndexModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    public IList<Danut_Sara_Lab2.Models.Authors> Authors { get; set; } =
    new List<Danut_Sara_Lab2.Models.Authors>();

    public async Task OnGetAsync()
    {
        Authors = await _context.Authors.Include(a => a.Books).ToListAsync();
    }
}
