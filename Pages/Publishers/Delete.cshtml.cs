
using Danut_Sara_Lab2.Data;
using Danut_Sara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Danut_Sara_Lab2.Pages.Publishers
{
    public class DeleteModel : PageModel
    {
        private readonly Danut_Sara_Lab2Context _context;

        public DeleteModel(Danut_Sara_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var publisher = await _context.Publisher
                .FirstOrDefaultAsync(p => p.ID == id);

            if (publisher == null)
                return NotFound();

            Publisher = publisher;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var publisher = await _context.Publisher.FindAsync(id);

            if (publisher != null)
            {
                _context.Publisher.Remove(publisher);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
