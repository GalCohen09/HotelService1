using AutoMapper;

namespace HotelService1.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Models.Reservation, Contract.Reservation>();
        }
    }
}
