using KoiCareSystemAtHome.Common;
using KoiCareSystemAtHome.Data;
using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Service.Bases;

namespace KoiCareSystemAtHome.Service;

public interface IKoiFishService
{
    Task<IServiceResult> GetAll();
    Task<IServiceResult> GetById(long id);
    Task<IServiceResult> UpdateById(long id, IServiceResult result);
    Task<IServiceResult> DeleteById(long id);
    Task<IServiceResult> Create();
    Task<IServiceResult> Save(KoiFish koiFish);
}


public class KoiFishService : IKoiFishService
{
    private readonly UnitOfWork _unitOfWork;

    public KoiFishService() => _unitOfWork ??= new UnitOfWork();

    public Task<IServiceResult> Create()
    {
        throw new NotImplementedException();
    }

    public async Task<IServiceResult> DeleteById(long id)
    {
        try
        {
            var result = false;
            var existingKoiFish = this.GetById(id);
            // Kiểm tra có tồn tại trước đó không
            if (existingKoiFish != null && existingKoiFish.Result.Status == Const.SUCCESS_READ_CODE)
            {
                // Nếu tồn tại ==> xóa
                result = await _unitOfWork.KoiFishRepository.RemoveAsync((KoiFish)existingKoiFish.Result.Data);
                if (result)
                {
                    // Xóa thành công => trả về kết quả 
                    return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, result);
                }
                else
                {
                    // Xóa không thành công => trả về lỗi  
                    return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, existingKoiFish.Result.Data);
                }
            }
            else
            {
                // Kiểm tra không tồn tại trước đó
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, result);
            }

            ////  ////////
            //if (existingKoiFish == null && existingKoiFish.Result.Status != Const.SUCCESS_READ_CODE)
            //{
            //    // Kiểm tra không tồn tại trước đó
            //    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, result);
            //}

        }
        catch (Exception ex)
        {
            return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    //public KoiFishService()
    //{
    //    _unitOfWork ??= new UnitOfWork();
    //}

    public async Task<IServiceResult> GetAll()
    {
        try
        {
            #region Business Rule

            #endregion
            var koiFishList = await _unitOfWork.KoiFishRepository.GetAllAsync();
            if (koiFishList == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koiFishList);
        }
        catch (Exception)
        {
            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
        }
    }

    public async Task<IServiceResult> GetById(long id)
    {
        try
        {
            #region Business Rule

            #endregion

            var koiFishList = await _unitOfWork.KoiFishRepository.GetByIdAsync(id);
            if (koiFishList == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koiFishList);
        }
        catch (Exception)
        {
            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
        }
    }

    public async Task<IServiceResult> Save(KoiFish koiFish)
    {
        try
        {
            int result = -1;
            var existingKoiFish = this.GetById(koiFish.FishId);

            // Update existing
            if (existingKoiFish.Result.Status == Const.SUCCESS_READ_CODE)
            {
                result = await _unitOfWork.KoiFishRepository.UpdateAsync(koiFish);

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, existingKoiFish);
            }

            // Create new object
            result = await _unitOfWork.KoiFishRepository.SaveAsync();
            return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, existingKoiFish);
        }
        catch (Exception ex)
        {
            return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public Task<IServiceResult> UpdateById(long id, IServiceResult result)
    {
        throw new NotImplementedException();
    }
}
