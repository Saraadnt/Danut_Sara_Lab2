using Danut_Sara_Lab2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danut_Sara_Lab2.Models;

namespace Danut_Sara_Lab2.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;
    public DetailsModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book.Include(b => b.Authors).Include(b => b.Publisher)
        .Include(b => b.BookCategories)
        .ThenInclude(bc => bc.Category)
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        else
        {
            Book = book;
        }

        return Page();
    }
}
