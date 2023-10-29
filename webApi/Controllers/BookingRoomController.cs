using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.DTOs;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRoomController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<BookingRoom> _repository;
        public BookingRoomController(AppDbContext context, IMapper mapper, IGenericRepository<BookingRoom> repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;

        }


        [HttpGet]
        public async Task<ActionResult<List<BookingRoomDto>>> GetAllBookedRooms()
        {
            var bookingrooms = await _repository.GetAll();

            var data = _mapper.Map<IReadOnlyList<BookingRoom>, IReadOnlyList<BookingRoomDto>>(bookingrooms);

            return new JsonResult(data);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BookingRoomDto>> GetById(int id)
        {
            var br = await _repository.GetByIdAsync(id);

            if (br == null)
            {
                return NotFound();
            }

            return Ok(br);
        }

        [HttpPost]
        public async Task<ActionResult<BookingRoomDto>> Create(BookingRoomDto DTO)
        {
            var Entity = _mapper.Map<BookingRoom>(DTO);
            var room = _context.Rooms.Find(Entity.RoomId);
            if (AddRoomToBooking(Entity, room))
            {
                await _repository.CreateAsync(Entity);
                return CreatedAtAction(nameof(GetById), new { id = Entity.Id }, DTO);
            }
            else
                return NotFound();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookingRoom>> Update(int id, BookingRoomDto Dto)
        {
            var Entity = _mapper.Map<BookingRoom>(Dto);
            Entity.Id = id;


            await _repository.UpdateAsync(Entity);


            var updatedDto = _mapper.Map<BookingRoomDto>(Entity);
            return Ok(updatedDto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }
        [NonAction]
        public bool AddRoomToBooking([FromBody] BookingRoom booking, [FromBody] Room room)
        {
            // Check if adding this room will exceed the maximum occupancy for the room type
            int maxOccupancy = GetMaxOccupancyForRoomType(room.Type);

            if (booking.NumberOfPersons <= maxOccupancy)
            {
                // Create a BookingRoom entry with the specified number of persons
                //var bookingRoom = new BookingRoom
                //{
                //    Booking = booking,
                //    Room = room,
                //    NumberOfPersons = numberOfPersons
                //};

                //booking.BookingRooms.Add(bookingRoom);

                return true;
            }
            return false;
        }
        [NonAction]
        private int GetMaxOccupancyForRoomType(RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.Single:
                    return RoomTypeMaxOccupancy.Standard;
                case RoomType.Double:
                    return RoomTypeMaxOccupancy.Double;
                case RoomType.Suite:
                    return RoomTypeMaxOccupancy.Suite;
                default:
                    return 0;
            }
        }
    }
}
