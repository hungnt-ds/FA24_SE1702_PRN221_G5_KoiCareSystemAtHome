using KoiCareSystemAtHome.Data.Models;
using KoiCareSystemAtHome.Data.Repository;

namespace KoiCareSystemAtHome.Data;

//public class UnitOfWork
//{
//    private FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _unitOfWorkContext;
//    private KoiFishRepository _koiFishRepository;
//    private PondRepository _pondRepository;
//    private UserRepository _userRepository;

//    public UnitOfWork()
//    {
//    }

//    public UnitOfWork(KoiFishRepository koiFishRepository, PondRepository pondRepository, UserRepository userRepository, FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext unitOfWorkContext)
//    {
//        _koiFishRepository = koiFishRepository;
//        _pondRepository = pondRepository;
//        _userRepository = userRepository;
//        _unitOfWorkContext = unitOfWorkContext;
//    }


//    //public UnitOfWork(FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext unitOfWorkContext, KoiFishRepository koiFishRepository)
//    //{
//    //    _unitOfWorkContext = unitOfWorkContext;
//    //    _koiFishRepository = koiFishRepository;
//    //}

//    //public UnitOfWork()
//    //{
//    //    _unitOfWorkContext ??= new FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext();
//    //}

//    //public KoiFishRepository KoiFishRepository { get; private set; }
//    //public PondRepository PondRepository { get; private set; }
//    //public UserRepository UserRepository { get; private set; }



//    public KoiFishRepository KoiFishRepository
//    {
//        get
//        {
//            return _koiFishRepository ??= new Repository.KoiFishRepository(_unitOfWorkContext);
//        }
//    }

//    public object PondRepository
//    {
//        get
//        {
//            return _pondRepository ??= new Repository.PondRepository(_unitOfWorkContext);
//        }
//    }

//    public object UserRepository
//    {
//        get
//        {
//            return _userRepository ??= new Repository.UserRepository(_unitOfWorkContext);
//        }
//    }
//}

public class UnitOfWork
{
    private readonly FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext _unitOfWorkContext;
    private KoiFishRepository _koiFishRepository;
    private PondRepository _pondRepository;
    private UserRepository _userRepository;

    public UnitOfWork() => _unitOfWorkContext ??= new FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext();
    public UnitOfWork(FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext unitOfWorkContext)
    {
        _unitOfWorkContext ??= new FA24_SE1702_PRN221_G5_KoiCareSystematHomeContext();
    }

    public KoiFishRepository KoiFishRepository
    {
        get
        {
            return _koiFishRepository ??= new KoiFishRepository(_unitOfWorkContext);
        }
    }

    public PondRepository PondRepository
    {
        get
        {
            return _pondRepository ??= new PondRepository(_unitOfWorkContext);
        }
    }

    public UserRepository UserRepository
    {
        get
        {
            return _userRepository ??= new UserRepository(_unitOfWorkContext);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _unitOfWorkContext.SaveChangesAsync();
    }
}
