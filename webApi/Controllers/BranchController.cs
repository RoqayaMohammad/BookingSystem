using AutoMapper;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webApi.DTOs;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Branch> _repository;
        public BranchController(AppDbContext context, IMapper mapper, IGenericRepository<Branch> repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;

        }
        
        
        [HttpGet]
        public async Task<ActionResult<List<DTOs.BranchDto>>> GetAllBranches()
        {
            var branches=await _repository.GetAll();
           
            var data = _mapper.Map<IReadOnlyList<Branch>, IReadOnlyList<BranchDto>>(branches);
            
            return new JsonResult(data);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDto>> Details(int id)
        {
            var branch = await _repository.GetByIdAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }

        [HttpPost]
        public async Task<ActionResult<BranchDto>> Create(BranchDto BranchDTO)
        {
            var BranchEntity = _mapper.Map<Branch>(BranchDTO);

            await _repository.CreateAsync(BranchEntity);
            
            return CreatedAtAction(nameof(Details), new { id = BranchEntity.Id }, BranchEntity);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Branch>> Update(int id, BranchDto branchDto)
        {
            var BranchEntity = _mapper.Map<Branch>(branchDto);
            BranchEntity.Id = id;
            

            await _repository.UpdateAsync( BranchEntity);

            
            var updatedBranchDto = _mapper.Map<BranchDto>(BranchEntity);
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
