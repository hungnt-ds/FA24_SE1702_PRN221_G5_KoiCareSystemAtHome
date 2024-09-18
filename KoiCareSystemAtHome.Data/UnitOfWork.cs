using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Data.Repository;

namespace KoiCareSystemAtHome.Data;

public class UnitOfWork
{
    private FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _unitOfWorkContext;
    private KoiFishRepository _koiFishRepository;

    //public UnitOfWork(FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext unitOfWorkContext, KoiFishRepository koiFishRepository)
    //{
    //    _unitOfWorkContext = unitOfWorkContext;
    //    _koiFishRepository = koiFishRepository;
    //}

    public UnitOfWork()
    {
        _unitOfWorkContext ??= new FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext();
    }

    public KoiFishRepository KoiFishRepository
    {
        get
        {
            return _koiFishRepository ??= new Repository.KoiFishRepository(_unitOfWorkContext);
        }
    }
}
