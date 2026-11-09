using Danut_Sara_Lab2.Data;
using Danut_Sara_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Danut_Sara_Lab2.Pages.Publishers
{
    public class DetailsModel : PageModel
    {
        private readonly Danut_Sara_Lab2Context _context;

        public DetailsModel(Danut_Sara_Lab2Context context)
        {
            _context = context;
        }

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
    }
}
