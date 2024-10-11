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

public interface IUserService
{
    Task<IServiceResult> GetAll();
}

public class UserService : IUserService
{
    private readonly UnitOfWork _unitOfWork;

    public UserService() => _unitOfWork ??= new UnitOfWork();
    public async Task<IServiceResult> GetAll()
    {
        try
        {
            #region Business Rule

            #endregion
            //var koiFishList = await _unitOfWork.KoiFishRepository.GetAllAsync();

            var userList = await _unitOfWork.UserRepository.GetAllAsync();

            if (userList == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, userList);
        }
        catch (Exception)
        {
            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
        }
    }
}
