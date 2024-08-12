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
    private IGenericRepository<Reviews> _Reviews;
    private IGenericRepository<Reservations> _Reservations;
    private IGenericRepository<Media> _Media;

    public IGenericRepository<ApplicationUser> _ApplicationUser;

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
    
	public IGenericRepository<Reviews> Reviews
    {
        get
        {

            if (_Reviews == null)
            {
                _Reviews = new GenericRepository<Reviews>(_dbContext);
            }

            return _Reviews;
        }
    }
    
    public IGenericRepository<Reservations> Reservations
    {
        get
        {

            if (_Reservations == null)
            {
                _Reservations = new GenericRepository<Reservations>(_dbContext);
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

