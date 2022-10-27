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
        public async Task<Region> Get(Guid id)
        {
            return await Context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Region> Add(Region region)
        {
            region.Id = new Guid();
            await Context.Regions.AddAsync(region);
            await Context.SaveChangesAsync();
            return region;
        }
        public async Task<Region> Delete(Guid id)
        {
            var region = await Context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null;
            }
            Context.Regions.Remove(region);
            await Context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> Update(Guid id, Region region)
        {
            var reg = await Context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (reg == null)
            {
                return null;
            }
            reg.Code = region.Code;
            reg.Name = region.Name;
            reg.Area = region.Area;
            reg.Lat = region.Lat;
            reg.Long = region.Long;
            reg.Population = region.Population;

            //await Context.Regions.Update(region);
            await Context.SaveChangesAsync();
            return reg;
        }
      
    }
}
