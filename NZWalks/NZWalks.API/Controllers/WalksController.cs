using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAll();
            var walkDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{id}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walk = await walkRepository.Get(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walksDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walksDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddWalkRequest request)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = request.Name,
                Length = request.Length,
                RegionId = request.RegionId,
                WalkDifficultyId = request.WalkDifficultyId
            };
            walk = await walkRepository.Add(walk);
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return CreatedAtAction(nameof(GetById), new { id = walk.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateWalkRequest request)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = request.Name,
                Length = request.Length,
                RegionId = request.RegionId,
                WalkDifficultyId = request.WalkDifficultyId
            };
            walk = await walkRepository.Update(id, walk);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.Delete(id);
            if(walk == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }
    }
}
