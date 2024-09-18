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
    public class DetailsModel : PageModel
    {
        private readonly KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _context;

        public DetailsModel(KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context)
        {
            _context = context;
        }

        public KoiFish KoiFish { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koifish = await _context.KoiFishes.FirstOrDefaultAsync(m => m.FishId == id);
            if (koifish == null)
            {
                return NotFound();
            }
            else
            {
                KoiFish = koifish;
            }
            return Page();
        }
    }
}
