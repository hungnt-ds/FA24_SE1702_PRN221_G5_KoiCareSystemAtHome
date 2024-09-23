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
public class PondRepository : GenericRepository<Pond>
{
    public PondRepository()
    {

    }

    public PondRepository(FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context) => _context = context;

}
