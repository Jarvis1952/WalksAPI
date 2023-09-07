using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository) 
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            // Map DTO to DomainModel
            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDTO);

            await _walkRepository.CreateAsync(walkDomainModel);

            //Map Domain model to DTO
            var walkDTO = _mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await _walkRepository.GetAllAsync();

            var walksDTO = _mapper.Map<List<WalkDTO>>(walksDomainModel);

            return Ok(walksDTO);    
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var WalkDomainModel = await _walkRepository.GetByIdAsync(id);
            
            if (WalkDomainModel == null)
                return NotFound();
            
            var walkDTO = _mapper.Map<WalkDTO>(WalkDomainModel);

            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDTO);

            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
                return NotFound();

            var walkDTO = _mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomain = await _walkRepository.DeleteAsync(id);

            if(deletedWalkDomain == null)
                return NotFound();

            var walkDto = _mapper.Map<WalkDTO>(deletedWalkDomain);

            return Ok(walkDto);
        }
    }
}
