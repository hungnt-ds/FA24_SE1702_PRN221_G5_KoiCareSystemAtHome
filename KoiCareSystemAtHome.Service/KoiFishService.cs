using KoiCareSystemAtHome.Common;
using KoiCareSystemAtHome.Data;
using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Service.Bases;
using Microsoft.EntityFrameworkCore;

namespace KoiCareSystemAtHome.Service;

public interface IKoiFishService
{
    Task<IServiceResult> GetAll();
    Task<IServiceResult> GetPonds();
    Task<IServiceResult> GetUsers();
    Task<IServiceResult> GetById(long id);
    Task<IServiceResult> UpdateById(long id, IServiceResult result);
    Task<IServiceResult> DeleteById(long id);
    Task<IServiceResult> Create(KoiFish koiFish);
    Task<IServiceResult> Save(KoiFish koiFish);
    Task<IServiceResult> SearchKoiFishAsync(string fishName, string pondName, string userName);
}


public class KoiFishService : IKoiFishService
{
    private readonly UnitOfWork _unitOfWork;

    public KoiFishService() => _unitOfWork ??= new UnitOfWork();

    //public KoiFishService(FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext context)
    //{
    //    _unitOfWork ??= new UnitOfWork(context);
    //}

    public async Task<IServiceResult> SearchKoiFishAsync(string fishName, string pondName, string email)
    {
        try
        {
            var query = _unitOfWork.KoiFishRepository.GetAllQueryableAsync();

            // Lọc theo Fish Name nếu có
            if (!string.IsNullOrEmpty(fishName))
            {
                query = query.Where(k => k.FishName.Contains(fishName));
            }

            // Lọc theo Pond Name nếu có
            if (!string.IsNullOrEmpty(pondName))
            {
                query = query.Where(k => k.Pond.PondName.Contains(pondName));
            }

            // Lọc theo User Name nếu có
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(k => k.User.Email.Contains(email));
            }

            var resultList = await query.ToListAsync();

            if (!resultList.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, resultList);
        }
        catch (Exception)
        {
            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
        }
    }



    //public async Task<IServiceResult> Create(KoiFish koiFish)
    //{
    //    try
    //    {
    //        int result = -1;
    //        //var existingKoiFish = this.GetById(koiFish.FishId);

    //        //// Update existing
    //        //if (existingKoiFish.Result.Status == Const.SUCCESS_READ_CODE)
    //        //{
    //        //    result = await _unitOfWork.KoiFishRepository.UpdateAsync(koiFish);

    //        //    return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, existingKoiFish);
    //        //}

    //        // Create new object
    //        await _unitOfWork.KoiFishRepository.CreateAsync(koiFish);
    //        result = await _unitOfWork.KoiFishRepository.SaveAsync();
    //        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
    //    }
    //    catch (Exception ex)
    //    {
    //        return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
    //    }
    //}

    public async Task<IServiceResult> Create(KoiFish koiFish)
    {
        try
        {
            koiFish.FishId = 0;  // Đặt về giá trị mặc định nếu FishId tự động tăng
            await _unitOfWork.KoiFishRepository.CreateAsync(koiFish);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceResult(Const.SUCCESS_CREATE_CODE, "KoiFish added successfully");
        }
        catch (Exception ex)
        {
            // Log lỗi hoặc kiểm tra exception
            return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message);
        }
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
            //var koiFishList = await _unitOfWork.KoiFishRepository.GetAllAsync();

            var koiFishList = await _unitOfWork.KoiFishRepository
                .GetAllQueryableAsync() // Assuming GetAll() is a method that returns IQueryable
                .Include(k => k.User) // Include related User
                .Include(k => k.Pond) // Include related Pond
                .ToListAsync();

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

    public async Task<IServiceResult> GetByIdWithIncludeAsync(long id)
    {
        try
        {
            #region Business Rule

            #endregion

            //var koiFishList = await _unitOfWork.KoiFishRepository.GetByIdWithIncludeAsync(id);
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
            var existingKoiFish = await this.GetById(koiFish.FishId);

            // Nếu đã tồn tại, tiến hành cập nhật
            if (existingKoiFish.Status == Const.SUCCESS_READ_CODE)
            {
                result = await _unitOfWork.KoiFishRepository.UpdateAsync(koiFish);
                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, koiFish);
                }
                return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
            else
            {
                // Nếu không tồn tại, tạo mới
                await _unitOfWork.KoiFishRepository.CreateAsync(koiFish);
                result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, koiFish);
                }
                return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
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

    public async Task<IServiceResult> Update(KoiFish koiFish)
    {
        //try
        //{
        //    int result = -1;
        //    var existingKoiFish = this.GetById(koiFish.FishId);

        //    // Update existing
        //    if (existingKoiFish.Result.Status == Const.SUCCESS_READ_CODE)
        //    {
        //        result = await _unitOfWork.KoiFishRepository.UpdateAsync(koiFish);

        //        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, existingKoiFish);
        //    }

        //    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);

        //    // Create new object
        //    //result = await _unitOfWork.KoiFishRepository.SaveAsync();
        //    //return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, existingKoiFish);
        //}
        //catch (Exception ex)
        //{
        //    return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
        //}
        try
        {
            int result = -1;
            var existingKoiFish = await this.GetById(koiFish.FishId); // Lấy thực thể đã theo dõi

            // Update existing
            if (existingKoiFish.Status == Const.SUCCESS_READ_CODE)
            {
                var koiFishToUpdate = (KoiFish) existingKoiFish.Data;  // Lấy thực thể từ kết quả
                if (koiFishToUpdate != null)
                {
                    // Cập nhật các thuộc tính của thực thể đã lấy ra với giá trị mới
                    _unitOfWork.KoiFishRepository.UpdateEntity(koiFishToUpdate, koiFish);

                    // Thực hiện lưu thay đổi thông qua Unit of Work
                    result = await _unitOfWork.SaveChangesAsync();

                    // Trả về kết quả cập nhật thành công
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, koiFishToUpdate);
                }
            }

            return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        }
        catch (Exception ex)
        {
            return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }

    //public async Task<ServiceResult<IEnumerable<User>>> GetUsers()
    //{
    //    try
    //    {
    //        var users = await _unitOfWork.KoiFishRepository.GetAllAsync(); // Giả sử _dbContext là đối tượng DbContext đã được khai báo
    //        if (users != null && users.Any())
    //        {
    //            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, users);
    //        }
    //        return new ServiceResult(Const.FAIL_READ_CODE, Const.SUCCESS_READ_MSG, "No users found.");
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log error if needed
    //        return new ServiceResult(Const.FAIL_READ_CODE, Const.SUCCESS_READ_MSG, "No users found.");
    //    }
    //}


    public async Task<IServiceResult> GetPonds()
    {
        var ponds = await _unitOfWork.PondRepository.GetAllAsync();
        return ponds != null
            ? new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, ponds)
            : new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
    }

    public async Task<IServiceResult> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        return users != null
            ? new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, users)
            : new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
    }
}
