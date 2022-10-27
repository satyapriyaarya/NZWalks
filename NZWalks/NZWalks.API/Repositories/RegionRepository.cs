using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext Context;
        public RegionRepository(NZWalksDBContext context)
        {
            Context = context;
        }


        public async Task<IEnumerable<Region>> GetAll()
        {
            return await Context.Regions.ToListAsync();
        }
    }
}
