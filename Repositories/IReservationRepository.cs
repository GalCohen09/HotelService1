using HotelService1.Models;

namespace HotelService1.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation> GetAsync(Guid id);
        Task<Reservation> AddAsync(Reservation reservation);

        Task<Reservation> DeleteAsync(Guid id);
        Task<Reservation> GetAsync(DateTime date);

    }
}
