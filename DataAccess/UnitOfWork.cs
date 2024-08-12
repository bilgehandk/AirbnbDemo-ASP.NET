using DataAccess;
using Infrastructure.Interfaces;
using Infrastructure.Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;  //dependency injection of Data Source

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IGenericRepository<Property> _Room;
    private IGenericRepository<Review> _Reviews;
    private IGenericRepository<Reservation> _Reservations;
    private IGenericRepository<Media> _Media;
    private IGenericRepository<AmenityType> _AmenityType;
    private IGenericRepository<Amenity> _Ammenity;
    private IGenericRepository<Calenderavailability> _CalenderAvaliability;
    private IGenericRepository<Fee> _Fee;
    private IGenericRepository<FeeType> _FeeType;
    private IGenericRepository<Prices> _Prices;
    private IGenericRepository<ReservationStatus> _ReservationStatus;
    
    public IGenericRepository<ApplicationUser> _ApplicationUser;
    
    public IGenericRepository<ReservationStatus> ReservationStatus
    {
        get
        {

            if (_ReservationStatus == null)
            {
                _ReservationStatus = new GenericRepository<ReservationStatus>(_dbContext);
            }

            return _ReservationStatus;
        }
    }
    
    public IGenericRepository<Prices> Prices
    {
        get
        {

            if (_Prices == null)
            {
                _Prices = new GenericRepository<Prices>(_dbContext);
            }

            return _Prices;
        }
    }
    
    public IGenericRepository<FeeType> FeeType
    {
        get
        {

            if (_FeeType == null)
            {
                _FeeType = new GenericRepository<FeeType>(_dbContext);
            }

            return _FeeType;
        }
    }
    
    public IGenericRepository<Fee> Fee
    {
        get
        {

            if (_Fee == null)
            {
                _Fee = new GenericRepository<Fee>(_dbContext);
            }

            return _Fee;
        }
    }
    
    public IGenericRepository<Calenderavailability> CalenderAvaliablity
    {
        get
        {

            if (_CalenderAvaliability == null)
            {
                _CalenderAvaliability = new GenericRepository<Calenderavailability>(_dbContext);
            }

            return _CalenderAvaliability;
        }
    }

    public IGenericRepository<AmenityType> AmenityType
    {
        get
        {
            if (_AmenityType == null)
            {
                _AmenityType = new GenericRepository<AmenityType>(_dbContext);
            }

            return _AmenityType;
        }
    }

    public IGenericRepository<Amenity> Ammenity
    {
        get
        {

            if (_Ammenity == null)
            {
                _Ammenity = new GenericRepository<Amenity>(_dbContext);
            }

            return _Ammenity;
        }
    }

    public IGenericRepository<Property> Room
    {
        get
        {

            if (_Room == null)
            {
                _Room = new GenericRepository<Property>(_dbContext);
            }

            return _Room;
        }
    }

	public IGenericRepository<ApplicationUser> ApplicationUser
	{
		get
		{

			if (_ApplicationUser == null)
			{
				_ApplicationUser = new GenericRepository<ApplicationUser>(_dbContext);
			}

			return _ApplicationUser;
		}
	}
    
	public IGenericRepository<Review> Reviews
    {
        get
        {

            if (_Reviews == null)
            {
                _Reviews = new GenericRepository<Review>(_dbContext);
            }

            return _Reviews;
        }
    }
    
    public IGenericRepository<Reservation> Reservations
    {
        get
        {

            if (_Reservations == null)
            {
                _Reservations = new GenericRepository<Reservation>(_dbContext);
            }

            return _Reservations;
        }
    }
    
    public IGenericRepository<Media> Media
    {
        get
        {

            if (_Media == null)
            {
                _Media = new GenericRepository<Media>(_dbContext);
            }

            return _Media;
        }
    }

    //ADD ADDITIONAL METHODS FOR EACH MODEL HERE

    public int Commit()
    {
        return _dbContext.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    //additional method added for garbage disposal

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}

