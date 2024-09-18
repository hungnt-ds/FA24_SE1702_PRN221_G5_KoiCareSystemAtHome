using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiCareSystemAtHome.Data.Models;

namespace KoiCareSystemAtHome.RazorWebApp.Pages.KoiFish
{
    public class IndexModel : PageModel
    {
        private readonly KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _context;

        public IndexModel(KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context)
        {
            _context = context;
        }

        public IList<KoiFish> KoiFish { get;set; } = default!;

        public async Task OnGetAsync()
        {
            KoiFish = await _context.KoiFishes
                .Include(k => k.Pond)
                .Include(k => k.User).ToListAsync();
        }
    }
}
