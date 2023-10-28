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
    public class RoomController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Room> _repository;
        public RoomController(AppDbContext context, IMapper mapper, IGenericRepository<Room> repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;

        }


        [HttpGet]
        public async Task<ActionResult<List<RoomDto>>> GetAllRooms()
        {
            var Rooms = await _repository.GetAll();

            var data = _mapper.Map<IReadOnlyList<Room>, IReadOnlyList<RoomDto>>(Rooms);

            return new JsonResult(data);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetById(int id)
        {
            var room = await _repository.GetByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> Create(RoomDto RoomDTO)
        {
            var RoomEntity = _mapper.Map<Room>(RoomDTO);

            await _repository.CreateAsync(RoomEntity);

            return CreatedAtAction(nameof(GetById), new { id = RoomDTO.Id }, RoomDTO);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Room>> Update(int id, RoomDto RoomDto)
        {
            var RoomEntity = _mapper.Map<Room>(RoomDto);
            RoomEntity.Id = id;


            await _repository.UpdateAsync(RoomEntity);


            var updatedBranchDto = _mapper.Map<RoomDto>(RoomEntity);
            return Ok(updatedBranchDto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
