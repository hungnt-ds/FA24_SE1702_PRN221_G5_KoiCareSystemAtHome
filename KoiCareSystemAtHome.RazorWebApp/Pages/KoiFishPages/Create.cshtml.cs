using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Service;

namespace KoiCareSystemAtHome.RazorWebApp.Pages.KoiFishPages
{
    public class CreateModel : PageModel
    {
        private readonly KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _context;

        public CreateModel(KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context, KoiFishService koiFishService)
        {
            _context = context;
            _koiFishService = koiFishService;
        }

        private readonly KoiFishService _koiFishService;

        public IActionResult OnGet()
        {
            var a = _context.Ponds.ToList();
            var b = _context.Users.ToList();
            ViewData["PondId"] = new SelectList(_context.Ponds, "PondId", "PondName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var a = _koiFishService.Create(KoiFish);
            //await _context.SaveChangesAsync();

            //var a = await _koiFishService.Save(KoiFish);

            return RedirectToPage("./Index");
        }
    }
}
