using Danut_Sara_Lab2.Data;
using Danut_Sara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Danut_Sara_Lab2.Pages.Books;

public class CreateModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public CreateModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");

        ViewData["AuthorsID"] = new SelectList(_context.Authors.Select(a => new
    {a.ID,FullName = a.FirstName + " " + a.LastName }),"ID","FullName");

        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {

        if (!ModelState.IsValid)
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName", Book.PublisherID);

            ViewData["AuthorsID"] = new SelectList(_context.Authors.Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName }),
                "ID", "FullName", Book.AuthorsID);

            return Page();
        }


        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
