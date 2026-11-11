using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danut_Sara_Lab2.Models;
using Danut_Sara_Lab2.Data;

namespace Danut_Sara_Lab2.Pages.BookCategoryPages;

public class DeleteModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public DeleteModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public BookCategory BookCategory { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var bookcategory = await _context.BookCategory.FirstOrDefaultAsync(m => m.ID == id);
        if (bookcategory is null)
        {
            return NotFound();
        }
        else
        {
            BookCategory = bookcategory;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var bookcategory = await _context.BookCategory.FindAsync(id);
        if (bookcategory != null)
        {
            BookCategory = bookcategory;
            _context.BookCategory.Remove(BookCategory);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
