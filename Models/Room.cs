using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace HotelService1.Models
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public int? FloorNumber { get; set; }
        public int? Size { get; set; }
        public string? ChosenBedType { get; set; }
        public bool? IsBalcony { get; set; }
        public int? PricePerNight { get; set; }
        //public Guid? BedTypeId { get; set; }

        //public virtual BedType? BedType { get; set; }

        public IList<ReservationRoom> ReservationsRooms { get; set; }

        private readonly IMemoryCache _memoryCache;

        public Room(IMemoryCache memoryCache) =>
            _memoryCache = memoryCache;

        public Room()
        {
        }

    }
}
