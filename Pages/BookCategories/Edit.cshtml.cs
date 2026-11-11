using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danut_Sara_Lab2.Models;
using Danut_Sara_Lab2.Data;

namespace Danut_Sara_Lab2.Pages.BookCategoryPages;

public class EditModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public EditModel(Danut_Sara_Lab2Context context)
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
        BookCategory = bookcategory;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(BookCategory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookCategoryExists(BookCategory.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BookCategoryExists(int id)
    {
        return _context.BookCategory.Any(e => e.ID == id);
    }
}
