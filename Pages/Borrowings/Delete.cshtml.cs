using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danut_Sara_Lab2.Models;
using Danut_Sara_Lab2.Data;

namespace Danut_Sara_Lab2.Pages.BorrowingPages;

public class DeleteModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public DeleteModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b.Authors)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (borrowing == null)
        {
            return NotFound();
        }

        Borrowing = borrowing;

        return Page();
    }


    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FindAsync(id);
        if (borrowing != null)
        {
            Borrowing = borrowing;
            _context.Borrowing.Remove(Borrowing);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
