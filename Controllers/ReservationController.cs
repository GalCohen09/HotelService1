using AutoMapper;
using HotelService1.Contract;
using HotelService1.Models;
using HotelService1.Repositories;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace HotelService1.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReservationController : Controller
    {    
        private static Logger logger = LogManager.GetLogger("hotelS");

        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;

        public ReservationController(IReservationRepository reservationRepository, IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var Reservations = await reservationRepository.GetAllAsync();
            var reservationContract = mapper.Map<List<Contract.Reservation>>(Reservations);
            return Ok(reservationContract);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetResevationAsync")]  
        public async Task<IActionResult> GetResevationAsync(Guid id)
        {
            var reservation = await reservationRepository.GetAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            var reservationContract = mapper.Map<Contract.Reservation>(reservation);   

            return Ok(reservationContract);

        }
        [HttpPost]
        public async Task<IActionResult> AddReservationAsync(AddReservationRequest addReservationRequest)
        {
            //request to the model
            var reservation = new Models.Reservation()
            {
                AccountHolderId = addReservationRequest.AccountHolderId,
                CustomerName = addReservationRequest.CustomerName,
                StartDate = addReservationRequest.StartDate,
                EndDate = addReservationRequest.EndDate,
                HasDiscount = addReservationRequest.HasDiscount,
                ReservationTotalAmount = addReservationRequest.ReservationTotalAmount,
            };

            //pass detail to repository
            reservation = await reservationRepository.AddAsync(reservation);

            //convert back to contract
            var reservationContract = new Contract.Reservation
            {
                Id = reservation.Id,
                AccountHolderId = reservation.AccountHolderId,
                CustomerName = reservation.CustomerName,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                HasDiscount = reservation.HasDiscount,
                ReservationTotalAmount = reservation.ReservationTotalAmount,
            };

            //beaucuse it is creating a resource we don't pass Ok
            return CreatedAtAction(nameof(GetResevationAsync), new {id = reservation.Id}, reservationContract); 
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteReservationAsync(Guid id)
        {
            //get reservation from db
            var reservation = await reservationRepository.DeleteAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            var reservationContract = new Contract.Reservation
            {
                Id = reservation.Id,
                AccountHolderId = reservation.AccountHolderId,
                CustomerName = reservation.CustomerName,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                HasDiscount = reservation.HasDiscount,
                ReservationTotalAmount = reservation.ReservationTotalAmount,
            };
            return Ok(reservationContract);
        }
    }
}
