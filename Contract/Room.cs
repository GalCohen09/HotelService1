using HotelService1.Models;

namespace HotelService1.Contract
{
    public class Room

    {
        public int RoomNumber { get; set; }
        public int? FloorNumber { get; set; }
        public int? Size { get; set; }
        public string? ChosenBedType { get; set; }
        public bool? IsBalcony { get; set; }
        public int? PricePerNight { get; set; }
        public Guid? BedType { get; set; }

        public virtual BedType? BedTypeNavigation { get; set; }
    }
}
