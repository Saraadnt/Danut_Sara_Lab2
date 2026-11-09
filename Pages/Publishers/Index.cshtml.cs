using Danut_Sara_Lab2.Data;
using Danut_Sara_Lab2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Danut_Sara_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Danut_Sara_Lab2Context _context;

        public IndexModel(Danut_Sara_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get; set; }
            = new List<Publisher>();

        public async Task OnGetAsync()
        {
            Publisher = await _context.Publisher.ToListAsync();
        }
    }
}
