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

    private IGenericRepository<PropertyInfo> _Property;
    private IGenericRepository<Review> _Reviews;
    private IGenericRepository<Reservation> _Reservations;
    private IGenericRepository<Amenity> _Ammenity;
    private IGenericRepository<CalenderAvailability> _CalenderAvaliability;
    private IGenericRepository<Fee> _Fee;
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
    
    public IGenericRepository<CalenderAvailability> CalenderAvaliablity
    {
        get
        {

            if (_CalenderAvaliability == null)
            {
                _CalenderAvaliability = new GenericRepository<CalenderAvailability>(_dbContext);
            }

            return _CalenderAvaliability;
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

    public IGenericRepository<PropertyInfo> Property
    {
        get
        {

            if (_Property == null)
            {
                _Property = new GenericRepository<PropertyInfo>(_dbContext);
            }

            return _Property;
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

