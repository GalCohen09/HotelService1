namespace HotelService1.Contract
{
    public class AddReservationRequest
    {
        public int? AccountHolderId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? HasDiscount { get; set; }
        public int? ReservationTotalAmount { get; set; }

    }
}
