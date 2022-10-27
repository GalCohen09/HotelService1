namespace HotelService1.Contract
{
    public class UpdateRoomRequest
    {
        public int? FloorNumber { get; set; }
        public int? Size { get; set; }
        public string? ChosenBedType { get; set; }
        public bool? IsBalcony { get; set; }
        public int? PricePerNight { get; set; }
    }
}
