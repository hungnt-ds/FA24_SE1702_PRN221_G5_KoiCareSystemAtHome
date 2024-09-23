//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using KoiCareSystemAtHome.Data.Models;
//using KoiCareSystemAtHome.Service;

//namespace KoiCareSystemAtHome.RazorWebApp.Pages.KoiFishPages
//{
//    public class EditModel : PageModel
//    {
//        //private readonly KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _context;

//        //public EditModel(KoiCareSystemAtHome.Data.Models.FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context)
//        //{
//        //    _context = context;
//        //}

//        private readonly KoiFishService _koiFishService;

//        public EditModel(KoiFishService koiFishService)
//        {
//            _koiFishService = koiFishService;
//        }

//        [BindProperty]
//        public KoiFish KoiFish { get; set; } = default!;

//        public async Task<IActionResult> OnGetAsync(long id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var koifish =  await _koiFishService.GetById(id);
//            if (koifish == null)
//            {
//                return NotFound();
//            }
//            KoiFish = (KoiFish)koifish.Data;
//           //ViewData["PondId"] = new SelectList(_koi.Ponds, "PondId", "PondId");
//           //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
//            return Page();
//        }

//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more information, see https://aka.ms/RazorPagesCRUD.
//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            //_context.Attach(KoiFish).State = EntityState.Modified;
//            _context.Attach(KoiFish).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!KoiFishExists(KoiFish.FishId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return RedirectToPage("./Index");
//        }

//        private bool KoiFishExists(long id)
//        {
//            //return _context.KoiFishes.Any(e => e.FishId == id);
//            var existingKoi = (_koiFishService.GetById(id)).WaitAsync();
//            return existingKoi.Data != null ? true : false;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Service;
using KoiCareSystemAtHome.Common;

namespace KoiCareSystemAtHome.RazorWebApp.Pages.KoiFishPages
{
    public class EditModel : PageModel
    {
        private readonly KoiFishService _koiFishService;

        public EditModel(KoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; } = default!;

        //public async Task<IActionResult> OnGetAsync(long id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var result = await _koiFishService.GetById(id);
        //    var result = await _koiFishService.GetByIdWithIncludeAsync(id);
        //    if (result.Data == null)
        //    {
        //        return NotFound();
        //    }
        //    KoiFish = (KoiFish)result.Data;

        //    return Page();

        //    //KoiFish = (KoiFish)KoiFish;
        //    //var pondsResult = await _koiFishService.GetPonds();
        //    //if (pondsResult.Status == Const.SUCCESS_READ_CODE && pondsResult.Data != null)
        //    //{
        //    //    var ponds = (IEnumerable<Pond>)pondsResult.Data;
        //    //    var pondIds = ponds.Select(p => new { PondId = p.PondId }).ToList();
        //    //    ViewData["PondId"] = new SelectList(pondIds, "PondId", "PondId", WaterParameter.PondId);
        //    //}
        //    //else
        //    //{
        //    //    ViewData["PondId"] = new SelectList(Enumerable.Empty<object>(), "PondId", "PondId");
        //    //}
        //    //return Page();
        //}

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    try
        //    {
        //        var updateResult = await _koiFishService.Update(KoiFish);
        //        if (updateResult.Status == Const.SUCCESS_UPDATE_CODE)
        //        {
        //            // Handle error or failure in the service layer
        //            return BadRequest(updateResult.Message);
        //        }
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await KoiFishExists(KoiFish.FishId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("./Index");
        //}

        //private async Task<bool> KoiFishExists(long id)
        //{
        //    var result = await _koiFishService.GetById(id);
        //    return result.Data != null;
        //}

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            // Lấy cá Koi theo id
            var result = await _koiFishService.GetByIdWithIncludeAsync(id);
            if (result.Data == null)
            {
                return NotFound();
            }
            KoiFish = (KoiFish)result.Data;

            // Lấy danh sách User
            var usersResult = await _koiFishService.GetUsers();
            if (usersResult.Status == Const.SUCCESS_READ_CODE && usersResult.Data != null && ((IEnumerable<User>)usersResult.Data).Any())
            {
                var users = (IEnumerable<User>)usersResult.Data;
                var userIds = users.Select(x =>  new { Id = x.Id }).ToList();
                ViewData["UserId"] = new SelectList(userIds, "Id", "Id", KoiFish.UserId);
            }
            else
            {
                // Nếu không có dữ liệu hoặc danh sách rỗng
                ViewData["UserId"] = new SelectList(Enumerable.Empty<object>(), "Id", "UserId");
            }

            // Lấy danh sách Pond
            var pondsResult = await _koiFishService.GetPonds();
            if (pondsResult.Status == Const.SUCCESS_READ_CODE && pondsResult.Data != null && ((IEnumerable<Pond>)pondsResult.Data).Any())
            {
                var ponds = (IEnumerable<Pond>)pondsResult.Data;

                var pondsId = ponds.Select(x => new { Id = x.PondId }).ToList();
                ViewData["PondId"] = new SelectList(pondsId, "PondId", "PondId", KoiFish.PondId);
            }
            else
            {
                // Nếu không có dữ liệu hoặc danh sách rỗng
                ViewData["PondId"] = new SelectList(Enumerable.Empty<object>(), "PondId", "PondId");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var updateResult = await _koiFishService.Update(KoiFish);
                if (updateResult.Status != Const.SUCCESS_UPDATE_CODE)
                {
                    //Handle error or failure in the service layer
                    return BadRequest(updateResult.Message);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await KoiFishExists(KoiFish.FishId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> KoiFishExists(long id)
        {
            var result = await _koiFishService.GetById(id);
            return result.Data != null;
        }
    }
}

