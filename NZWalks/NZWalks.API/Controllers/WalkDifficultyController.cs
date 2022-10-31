using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyReposiroty;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyReposiroty, IMapper mapper)
        {
            this.walkDifficultyReposiroty = walkDifficultyReposiroty;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var wd = await walkDifficultyReposiroty.GetAll();
            return Ok(wd);
        }

        [HttpGet]
        [Route("{id}")]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var wd = await walkDifficultyReposiroty.Get(id);
            if(wd == null)
            {
                return NotFound();
            }
            return Ok(wd);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddWalkDifficultyRequest request)
        {
            var wdDomain = new Models.Domain.WalkDifficulty()
            {
                Code = request.Code
            };
            wdDomain = await walkDifficultyReposiroty.Add(wdDomain);
            return CreatedAtAction(nameof(GetById), new {Id = wdDomain.Id}, wdDomain);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateWalkDifficultyRequest request)
        {
            var wdDomain = new Models.Domain.WalkDifficulty()
            {
                Code = request.Code
            };
            wdDomain = await walkDifficultyReposiroty.Update(id, wdDomain);
            if (wdDomain == null)
            {
                return NotFound();
            }
            return Ok(wdDomain);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var wd = await walkDifficultyReposiroty.Delete(id);
            if(wd == null)
            {
                return NotFound();
            }
            return Ok(wd);
        }
    }
}
