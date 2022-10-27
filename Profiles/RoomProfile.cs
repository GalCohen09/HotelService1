using AutoMapper;

namespace HotelService1.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Models.Room, Contract.Room>();
        }
    }
}
