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

    public BookData BookD { get; set; } = default!;
    public int BookID { get; set; }
    public int CategoryID { get; set; }

    public string TitleSort { get; set; } = string.Empty;
    public string AuthorSort { get; set; } = string.Empty;

    public string CurrentFilter { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public int? SelectedAuthorsID { get; set; }

    public SelectList AuthorsList { get; set; } = default!;

    public async Task OnGetAsync(int? id, int? categoryID, string? sortOrder, string? searchString)
    {
        BookD = new BookData();

        AuthorsList = new SelectList(
    await _context.Authors
        .OrderBy(a => a.LastName)
        .ThenBy(a => a.FirstName)
        .ToListAsync(),
    "ID",
    "FullName",
    SelectedAuthorsID
);

        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        CurrentFilter = searchString ?? string.Empty;

        BookD.Books = await _context.Book
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        if (SelectedAuthorsID.HasValue)
        {
            BookD.Books = BookD.Books
                .Where(b => b.AuthorsID == SelectedAuthorsID.Value);
        }

        if (!String.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books.Where(s =>
                (s.Authors != null &&
                    (s.Authors.FirstName.Contains(searchString) ||
                     s.Authors.LastName.Contains(searchString)))
                || s.Title.Contains(searchString));
        }

        if (id != null)
        {
            BookID = id.Value;
            Book book = BookD.Books
                .Where(i => i.ID == id.Value)
                .Single();

            BookD.Categories = book.BookCategories
                .Select(s => s.Category);
        }

        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                break;

            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(s => s.Authors?.FullName);
                break;

            case "author":
                BookD.Books = BookD.Books.OrderBy(s => s.Authors?.FullName);
                break;

            default:
                BookD.Books = BookD.Books.OrderBy(s => s.Title);
                break;
        }
    }



}
