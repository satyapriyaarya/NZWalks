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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalksController(
            IWalkRepository walkRepository, 
            IRegionRepository regionRepository,
            IWalkDifficultyRepository walkDifficultyRepository,
            IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
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
            var valid = await ValidateAdd(request);
            if (!valid)
            {
                return BadRequest(ModelState);
            }
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

        private async Task<bool> ValidateAdd(AddWalkRequest addWalkRequest)
        {
            if (string.IsNullOrEmpty(addWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(addWalkRequest), "Name must not be null.");
            }

            if (addWalkRequest.Length < 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest), "Length can't be less than 0.");
            }

            var region = await regionRepository.Get(addWalkRequest.RegionId);
            if(region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest), "Region must exists.");
            }

            var wd = await walkDifficultyRepository.Get(addWalkRequest.WalkDifficultyId);
            if (wd == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest), "Walk difficulty must exists.");
            }

            if (ModelState.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
