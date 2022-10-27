
using AutoMapper;
using HotelService1.Contract;
using HotelService1.Models;
using HotelService1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HotelService1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RoomController> logger;
        private readonly IMemoryCache memoryCache;

        public RoomController(IRoomRepository roomRepository
            , IMapper mapper
            , ILogger<RoomController> logger
            , IMemoryCache memoryCache)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
           
            try
            {
                List<Contract.Room> roomsContract;
                if (memoryCache.TryGetValue("rooms", out roomsContract))
                {
                    logger.LogInformation("found in cache.");
                        //(LogLevel.Information, "found in cache.");
                }
                else
                {
                    var Rooms = await roomRepository.GetAllAsync();
                    /////////instead of this/////////////
                    //var roomsContract = new List<Contract.Room>();
                    //Rooms.ToList().ForEach(room =>
                    //{
                    //    var roomContract = new Contract.Room()
                    //    {
                    //        RoomNumber = room.RoomNumber,
                    //        FloorNumber = room.FloorNumber,
                    //        Size = room.Size,
                    //        ChosenBedType = room.ChosenBedType,
                    //        IsBalcony = room.IsBalcony,
                    //        PricePerNight = room.PricePerNight,

                    //    };
                    //    roomsContract.Add(roomContract);
                    //});
                    ////////we use IMapper//////////////

                    roomsContract = mapper.Map<List<Contract.Room>>(Rooms);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                   .SetPriority(CacheItemPriority.Normal)
                   .SetSize(1024);

                    memoryCache.Set("rooms", roomsContract, cacheEntryOptions);

                }
                return Ok(roomsContract);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            
        }


        [HttpGet]
        [Route("{roomNumber:int}")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetRoomAsync(int roomNumber)
        {
            try
            {
                var room = await roomRepository.GetAsync(roomNumber);
                if (room == null)
                {
                    return NotFound();
                }
                var roomContract = mapper.Map<Contract.Room>(room);

                return Ok(roomContract);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddRoomAsync(Models.Room room)
        {

            //pass detail to repository
            room = await roomRepository.AddAsync(room);

            //request to the model
            room = new Models.Room()
            {
                RoomNumber = room.RoomNumber,
                FloorNumber = room.FloorNumber,
                Size = room.Size,
                ChosenBedType = room.ChosenBedType,
                IsBalcony = room.IsBalcony,
                PricePerNight = room.PricePerNight,
            };


            return Ok(room);

        }

        [HttpPut]
        [Route("{roomNum:int}")]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute]int roomNum,[FromBody] Contract.UpdateRoomRequest updateRoomRequest)
        {

            var room = new Models.Room()
            {
                FloorNumber = updateRoomRequest.FloorNumber,
                Size = updateRoomRequest.Size,
                ChosenBedType = updateRoomRequest.ChosenBedType,
                IsBalcony = updateRoomRequest.IsBalcony,
                PricePerNight = updateRoomRequest.PricePerNight,
            };
            room = await roomRepository.UpdateAsync(roomNum, room);

            if (room == null) return NotFound();

            var roomContract = new Contract.Room
            {
                FloorNumber = room.FloorNumber,
                Size = room.Size,
                ChosenBedType = room.ChosenBedType,
                IsBalcony = room.IsBalcony,
                PricePerNight = room.PricePerNight,
            };
            return Ok(roomContract);
        }

        //[HttpGet]
        //[Route("{reservationId:Guid}")]
        //public async Task<IActionResult> GetAvailableRooms(Guid reservationId)
        //{
        //    var room = await roomRepository.GetAvailableRooms(reservationId);
        //    if (room == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(room);
        //}
        [HttpGet]
        [Route("GetAvailableRoomsByDates")]
        public IActionResult GetAvailableRoomsByDates(DateTime givenStartDate, DateTime givenEndDate)
        {
            var room =  roomRepository.GetAvailableRooms(givenStartDate, givenEndDate);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

    }
}
