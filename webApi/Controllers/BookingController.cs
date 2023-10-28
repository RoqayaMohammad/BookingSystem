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
    public class BookingController : ControllerBase
    {
       
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            private readonly IGenericRepository<Booking> _repository;
            public BookingController(AppDbContext context, IMapper mapper, IGenericRepository<Booking> repository)
            {
                _context = context;
                _mapper = mapper;
                _repository = repository;

            }


            [HttpGet]
            public async Task<ActionResult<List<BookindDTO>>> GetAllBooking()
            {
                var Bookings = await _repository.GetAll();

                var data = _mapper.Map<IReadOnlyList<Booking>, IReadOnlyList<BookindDTO>>(Bookings);

                return new JsonResult(data);

            }


            [HttpGet("{id}")]
            public async Task<ActionResult<BookindDTO>> GetById(int id)
            {
                var Booking = await _repository.GetByIdAsync(id);

                if (Booking == null)
                {
                    return NotFound();
                }

                return Ok(Booking);
            }

            [HttpPost]
            public async Task<ActionResult<BookindDTO>> Create(BookindDTO BookindDTO)
            {
                var BookingEntity = _mapper.Map<Booking>(BookindDTO);

                await _repository.CreateAsync(BookingEntity);

                return CreatedAtAction(nameof(GetById), new { id = BookindDTO.Id }, BookindDTO);


            }

            [HttpPut("{id}")]
            public async Task<ActionResult<Booking>> Update(int id, BookindDTO Dto)
            {
                var Entity = _mapper.Map<Booking>(Dto);
                Entity.Id = id;


                await _repository.UpdateAsync(Entity);


                var updatedDto = _mapper.Map<BookindDTO>(Entity);
                return Ok(updatedDto);

            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(int id)
            {
                await _repository.DeleteAsync(id);

                return NoContent();
            }
        
    }
}
