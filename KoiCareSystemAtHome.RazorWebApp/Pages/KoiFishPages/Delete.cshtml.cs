using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSystemAtHome.RazorWebApp.Pages.KoiFishPages
{
    public class DeleteModel : PageModel
    {
        //private readonly KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _context;

        //public DeleteModel(KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context)
        //{
        //    _context = context;
        //}

        private readonly KoiFishService _koiFishService;

        public DeleteModel(KoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koifish = await _koiFishService.GetById(id);

            var result = (KoiFish)koifish.Data;

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                KoiFish = result;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koifish = await _koiFishService.GetById(id);
            var result = (KoiFish)koifish.Data;

            if (result != null)
            {
                KoiFish = result;
                _koiFishService.DeleteById(id);
                //_context.KoiFishes.Remove(KoiFish);
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
