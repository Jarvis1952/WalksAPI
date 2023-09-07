using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper) { 
            _dbContext = dbContext;
            _mapper = mapper;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            var regionsDTO = _mapper.Map<List<RegionDTO>>(regionsDomain);

            return Ok(regionsDTO);        
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
                return NotFound();

            var regionDTO = _mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDTO);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDTO);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null)
                return NotFound();

            var regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if( regionDomainModel == null)
                return NotFound();

            var regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }
    }
}
