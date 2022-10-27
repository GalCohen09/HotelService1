using System;
using System.Collections.Generic;

namespace HotelService1.Models
{
    public class ReservationRoom
    {
       // public int Id { get; set; }
        public Guid ReservationId { get; set; }
        public int RoomNumber { get; set; }

        public Reservation Reservation { get; set; }
        //public Room Room { get; set; }



    }
}
