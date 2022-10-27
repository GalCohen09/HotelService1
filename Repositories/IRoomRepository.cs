using HotelService1.Models;

namespace HotelService1.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetAsync(int RoomNumber);

        Task<Room> AddAsync(Room room);
        Task<Room> UpdateAsync(int roomNum, Room room);
        //Task<IEnumerable<Room>> GetAvailableRooms(Guid reservationId);
        List<Room> GetAvailableRooms(DateTime givenStartDate, DateTime givenEndDate);

    }
}
