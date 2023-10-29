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
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Customer> _repository;
        public CustomerController(AppDbContext context, IMapper mapper, IGenericRepository<Customer> repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;

        }


        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomers()
        {
            var customers = await _repository.GetAll();

            var data = _mapper.Map<IReadOnlyList<Customer>, IReadOnlyList<CustomerDto>>(customers);

            return new JsonResult(data);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CustomerDto DTO)
        {
            var Entity = _mapper.Map<Customer>(DTO);

            await _repository.CreateAsync(Entity);

            return CreatedAtAction(nameof(GetById), new { id =Entity.CustomerId }, DTO);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(int id, CustomerDto Dto)
        {
            var Entity = _mapper.Map<Customer>(Dto);
            Entity.CustomerId = id;


            await _repository.UpdateAsync(Entity);


            var updatedDto = _mapper.Map<CustomerDto>(Entity);
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
