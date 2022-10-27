using System;
using System.Collections.Generic;

namespace HotelService1.Models
{
    public class BedType
    {
        public BedType()
        {
            //Rooms = new HashSet<Room>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }

        //public virtual ICollection<Room> Rooms { get; set; }
    }
}
