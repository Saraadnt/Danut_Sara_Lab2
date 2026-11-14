using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danut_Sara_Lab2.Models;
using Danut_Sara_Lab2.Data;

namespace Danut_Sara_Lab2.Pages.MemberPages;

public class IndexModel : PageModel
{
    private readonly Danut_Sara_Lab2Context _context;

    public IndexModel(Danut_Sara_Lab2Context context)
    {
        _context = context;
    }

    public IList<Member> Member { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Member = await _context.Member.ToListAsync();
    }
}
