using AutoMapper;
using HotelService1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;

namespace HotelService1.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly PayoneerMailingContext pmContextForRoom;
        private readonly IMapper Mapper;
        public RoomRepository(PayoneerMailingContext payoneerMailingContext, IMapper mapper)
        {
            this.pmContextForRoom = payoneerMailingContext;
            this.Mapper = mapper;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await pmContextForRoom.Rooms.ToListAsync();
        }

        public async Task<Room> GetAsync(int roomNumber)
        {
            return await pmContextForRoom.Rooms.FirstOrDefaultAsync(x => x.RoomNumber == roomNumber);
        }
        public async Task<Room> AddAsync(Room room)
        {
            await pmContextForRoom.AddAsync(room);
            await pmContextForRoom.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateAsync(int roomNum, Room room)
        {
            var existingRoom = await pmContextForRoom.Rooms.FirstOrDefaultAsync(x => x.RoomNumber == roomNum);
            if (existingRoom == null) return null;
            existingRoom.FloorNumber = room.FloorNumber;
            existingRoom.Size = room.Size;
            existingRoom.ChosenBedType = room.ChosenBedType;
            existingRoom.IsBalcony = room.IsBalcony;
            existingRoom.PricePerNight = room.PricePerNight;

            await pmContextForRoom.SaveChangesAsync();
            return existingRoom;
        }

        //public async Task<IEnumerable<Room>> GetAvailableRooms(Guid reservationId)
        //{
        //    var reservation = await pmContextForRoom.Reservations.Where(x => x.Id == reservationId)
        //        .Include(b => b.ReservationsRooms)
        //        .ThenInclude(x => x.Room)
        //        .FirstOrDefaultAsync();
        //    if (reservation == null)
        //    {
        //        return null;

        //    }
        //    var availableRooms =  reservation.ReservationsRooms.Select(x => x.Room);
        //    return availableRooms;

        //}

        //public async Task<IEnumerable<Room>> GetAvailableRooms(Guid reservationId)
        //{
        //    var reservation = await pmContextForRoom.Reservations.SingleAsync(x => x.Id == reservationId);

        //    var availableRooms = await pmContextForRoom.Rooms
        //        .Include(x => x.ReservationsRooms)
        //        .ThenInclude(x => x.Reservation)
        //        .ToListAsync();

        //    return availableRooms;
        //}

        public List<Room> GetAvailableRooms(DateTime givenStartDate, DateTime givenEndDate)
        {
         //   var reservations = pmContextForRoom.Reservations.Where(x => x.StartDate == givenStartDate && x.EndDate == givenEndDate);

            var AvailableRooms = pmContextForRoom.Rooms
                .Include(x => x.ReservationsRooms)
                .ThenInclude(x => x.Reservation)
                .Where(x => x.ReservationsRooms.FirstOrDefault(y => (y.Reservation.StartDate <= givenStartDate && y.Reservation.EndDate >= givenStartDate)
                ||( y.Reservation.StartDate <= givenEndDate && y.Reservation.EndDate >= givenEndDate)) == null)
               // .Select(x => x.RoomNumber) --> is we want to return only the number of the room
                .ToList();

            return AvailableRooms;
        }

        //public async Task<IEnumerable<Room>> GetAvailableRooms()
        //{
        //    var allRooms = await GetAllAsync();
        //    if (allRooms == null)
        //    {
        //        return null;

        //    }
        //    var allTakenRooms = await pmContextForRoom.ReservationRooms.ToListAsync();
        //    if (allTakenRooms == null)
        //    {
        //        return allRooms;
        //    }
        //    allTakenRooms.Select(x=>x.RoomNumber).Contains(allRooms.Select(x=>x.RoomNumber));  

        //    var availableRooms = 
        //    return availableRooms;

        //}
    }
}
