using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danut_Sara_Lab2.Models;
using Danut_Sara_Lab2.Data;

namespace Danut_Sara_Lab2.Pages.AuthorsPages;

public class EditModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public EditModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Danut_Sara_Lab2.Models.Authors Authors { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var authors = await _context.Authors.FirstOrDefaultAsync(m => m.ID == id);
        if (authors is null)
        {
            return NotFound();
        }
        Authors = authors;
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

        _context.Attach(Authors).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorsExists(Authors.ID))
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

    private bool AuthorsExists(int id)
    {
        return _context.Authors.Any(e => e.ID == id);
    }
}
