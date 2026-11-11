using Danut_Sara_Lab2.Data;
using Danut_Sara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Danut_Sara_Lab2.Pages.Books;

public class EditModel : BookCategoriesPageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public EditModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book.Include(b => b.Authors).Include(b => b.Publisher).Include(b => b.BookCategories)
        .ThenInclude(bc => bc.Category).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        PopulateAssignedCategoryData(_context, Book);

        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
"PublisherName");

        ViewData["AuthorsID"] = new SelectList(_context.Authors.Select(a => new
    {a.ID, FullName = a.FirstName + " " + a.LastName}), "ID", "FullName");

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int? id, string[]
selectedCategories)
    {
        {
            if (id == null)
            {
                return NotFound();
            }

            //se va include Author conform cu sarcina de la lab 2
            var bookToUpdate = await _context.Book.Include(b => b.Authors).Include(b => b.Publisher).Include(b => b.BookCategories)
        .ThenInclude(bc => bc.Category).FirstOrDefaultAsync(b => b.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            // se va modifica AuthorID conform cu sarcina de la lab 2
            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title, i => i.AuthorsID,
                i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }


            ViewData["PublisherID"] = new SelectList(
                _context.Set<Publisher>(),
                "ID",
                "PublisherName",
                bookToUpdate.PublisherID);

            ViewData["AuthorsID"] = new SelectList(
                _context.Authors.Select(a => new
                {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                }),
                "ID",
                "FullName",
                bookToUpdate.AuthorsID);

            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);

            return Page();
        }
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
