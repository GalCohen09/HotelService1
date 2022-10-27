using AutoMapper;
using HotelService1.Models;
using Microsoft.EntityFrameworkCore;
namespace HotelService1.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly PayoneerMailingContext pmContext;
        private readonly IMapper Mapper;

        public ReservationRepository(PayoneerMailingContext payoneerMailingContext, IMapper mapper)
        {
            this.pmContext = payoneerMailingContext;
            this.Mapper = mapper;

        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await pmContext.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetAsync(Guid id)
        {
            return await pmContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            reservation.Id = Guid.NewGuid();
            await pmContext.AddAsync(reservation);
            await pmContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> DeleteAsync(Guid id)
        {
            var reservation = await pmContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
            if (reservation == null)
            {
                return null;
            }

            pmContext.Reservations.Remove(reservation);
            await pmContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> GetAsync(DateTime date)
        {
            return await pmContext.Reservations.FirstOrDefaultAsync(x => x.StartDate <= date && x.EndDate >= date);

        }
    }
}
