using Danut_Sara_Lab2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danut_Sara_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Danut_Sara_Lab2.Pages.Books;

public class IndexModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public IndexModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? SelectedAuthorsID { get; set; }

    public SelectList AuthorsList { get; set; } = default!;


    public async Task OnGetAsync()
    {
        AuthorsList = new SelectList(
            await _context.Authors
                .Select(a => new
                {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToListAsync(),
            "ID",
            "FullName"
        );

        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .AsQueryable();

        if (SelectedAuthorsID.HasValue)
        {
            books = books.Where(
                b => b.AuthorsID == SelectedAuthorsID.Value
            );
        }

        Book = await books.ToListAsync();
    }


}
