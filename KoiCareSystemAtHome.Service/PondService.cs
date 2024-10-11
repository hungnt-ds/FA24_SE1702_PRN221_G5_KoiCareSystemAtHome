using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiCareSystemAtHome.Common;
using KoiCareSystemAtHome.Data;
using KoiCareSystemAtHome.Service.Bases;
using Microsoft.EntityFrameworkCore;

namespace KoiCareSystemAtHome.Service;

public interface IPondService
{
    Task<IServiceResult> GetAll();
}

public class PondService : IPondService
{
    private readonly UnitOfWork _unitOfWork;

    public PondService() => _unitOfWork ??= new UnitOfWork();
    public async Task<IServiceResult> GetAll()
    {
        try
        {
            #region Business Rule

            #endregion
            //var koiFishList = await _unitOfWork.KoiFishRepository.GetAllAsync();

            var pondList = await _unitOfWork.PondRepository.GetAllAsync();

            if (pondList == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, pondList);
        }
        catch (Exception)
        {
            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
        }
    }
}
