using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        //ADD Models/Tables here as you create them so UnitOfWork will have access
        public IGenericRepository<ApplicationUser> ApplicationUser { get; }
        public IGenericRepository<PropertyInfo> Property { get; }
        public IGenericRepository<Reservation> Reservations { get; }
        public IGenericRepository<Review> Reviews { get; }
        public IGenericRepository<Amenity> Ammenity { get; }
        public IGenericRepository<CalenderAvailability> CalenderAvaliablity { get; }
        public IGenericRepository<Fee> Fee { get; }
        public IGenericRepository<ReservationStatus> ReservationStatus { get; }

        //save changes to the data source

        int Commit();

        Task<int> CommitAsync();

    }
}