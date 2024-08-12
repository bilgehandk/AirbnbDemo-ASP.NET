using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        //ADD Models/Tables here as you create them so UnitOfWork will have access
        public IGenericRepository<ApplicationUser> ApplicationUser { get; }
        public IGenericRepository<Property> Room { get; }
        public IGenericRepository<Reservations> Reservations { get; }
        public IGenericRepository<Reviews> Reviews { get; }
        public IGenericRepository<Media> Media { get; }

        //save changes to the data source

        int Commit();

        Task<int> CommitAsync();

    }
}