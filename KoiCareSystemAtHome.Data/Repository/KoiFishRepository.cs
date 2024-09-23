using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiCareSystemAtHome.Data.Base;
using KoiCareSystemAtHome.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KoiCareSystemAtHome.Data.Repository;
public class KoiFishRepository : GenericRepository<KoiFish>
{
    public KoiFishRepository()
    {

    }

    public KoiFishRepository(FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context) => _context = context;

    public void UpdateEntity(KoiFish existingEntity, KoiFish newEntity)
    {
        _context.Entry(existingEntity).CurrentValues.SetValues(newEntity);
    }
}
