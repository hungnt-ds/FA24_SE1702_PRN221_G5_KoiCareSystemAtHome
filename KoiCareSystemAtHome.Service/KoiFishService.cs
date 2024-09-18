using KoiCareSystemAtHome.Common;
using KoiCareSystemAtHome.Data;
using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Service.Bases;

namespace KoiCareSystemAtHome.Service;

public interface IKoiFishService
{
    Task<BusinessResult> GetAll();
    Task<BusinessResult> GetById(long id);
    Task<BusinessResult> UpdateById(long id, BusinessResult result);
    Task<BusinessResult> DeleteById(long id);
    Task<BusinessResult> Create();
    Task<BusinessResult> Save(KoiFish koiFish);
}


public class KoiFishService : IKoiFishService
{
    private readonly UnitOfWork _unitOfWork;

    public KoiFishService() => _unitOfWork ??= new UnitOfWork();

    public Task<BusinessResult> Create()
    {
        throw new NotImplementedException();
    }

    public async Task<BusinessResult> DeleteById(long id)
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
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, result);
                }
                else
                {
                    // Xóa không thành công => trả về lỗi  
                    return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, existingKoiFish.Result.Data);
                }
            }
            else
            {
                // Kiểm tra không tồn tại trước đó
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, result);
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
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    //public KoiFishService()
    //{
    //    _unitOfWork ??= new UnitOfWork();
    //}

    public async Task<BusinessResult> GetAll()
    {
        try
        {
            #region Business Rule

            #endregion
            var koiFishList = await _unitOfWork.KoiFishRepository.GetAllAsync();
            if (koiFishList == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koiFishList);
        }
        catch (Exception)
        {
            return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
        }
    }

    public async Task<BusinessResult> GetById(long id)
    {
        try
        {
            #region Business Rule

            #endregion

            var koiFishList = await _unitOfWork.KoiFishRepository.GetByIdAsync(id);
            if (koiFishList == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, koiFishList);
        }
        catch (Exception)
        {
            return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
        }
    }

    public async Task<BusinessResult> Save(KoiFish koiFish)
    {
        try
        {
            int result = -1;
            var existingKoiFish = this.GetById(koiFish.FishId);

            // Update existing
            if (existingKoiFish.Result.Status == Const.SUCCESS_READ_CODE)
            {
                result = await _unitOfWork.KoiFishRepository.UpdateAsync(koiFish);

                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, existingKoiFish);
            }

            // Create new object
            result = await _unitOfWork.KoiFishRepository.SaveAsync();
            return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, existingKoiFish);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    public Task<BusinessResult> UpdateById(long id, BusinessResult result)
    {
        throw new NotImplementedException();
    }
}
