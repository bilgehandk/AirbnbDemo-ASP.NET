using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        //ADD Models/Tables here as you create them so UnitOfWork will have access
        public IGenericRepository<ApplicationUser> ApplicationUser { get; }
        public IGenericRepository<Property> Room { get; }
        public IGenericRepository<Reservation> Reservations { get; }
        public IGenericRepository<Review> Reviews { get; }
        public IGenericRepository<Media> Media { get; }
        public IGenericRepository<AmenityType> AmenityType { get; }
        public IGenericRepository<Ammenity> Ammenity { get; }
        public IGenericRepository<CalenderAvaliability> CalenderAvaliablity { get; }
        public IGenericRepository<Fee> Fee { get; }
        public IGenericRepository<FeeType> FeeType { get; }
        public IGenericRepository<Prices> Prices { get; }
        public IGenericRepository<ReservationStatus> ReservationStatus { get; }

        //save changes to the data source

        int Commit();

        Task<int> CommitAsync();

    }
}