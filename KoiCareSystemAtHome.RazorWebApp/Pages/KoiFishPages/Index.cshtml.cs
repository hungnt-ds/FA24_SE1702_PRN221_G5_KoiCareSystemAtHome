using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Service;

namespace KoiCareSystemAtHome.RazorWebApp.Pages.KoiFishPages
{
    public class IndexModel : PageModel
    {
        //private readonly KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _context;
        private readonly KoiFishService _koiFishService;

        //public IndexModel(KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context, KoiFishService koiFishService)
        //{
        //    _context = context;
        //    _koiFishService = koiFishService;
        //}
        public IndexModel(KoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        public IList<KoiFish> KoiFish { get; set; } = default!;

        public async Task OnGetAsync()
        {
            //KoiFish = await _context.KoiFishes
            //    .Include(k => k.Pond)
            //    .Include(k => k.User).ToListAsync();

            //var result = await _koiFishService.GetAll();

            //KoiFish = (IList<KoiFish>)result.Data;

            // Dòng này dòng chuẩn rồi ---->
            KoiFish = (await _koiFishService.GetAll()).Data as IList<KoiFish>;
        }
    }
}
