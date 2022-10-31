using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext context;

        public WalkRepository(NZWalksDBContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Walk>> GetAll()
        {
            return await 
                context.Walks.
                Include(x=>x.Region).
                Include(x=>x.WalkDifficulty).
                ToListAsync();
        }
        public async Task<Walk> Get(Guid id)
        {
            return await 
                context.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> Add(Walk walk)
        {
            walk.Id = new Guid();
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> Delete(Guid id)
        {
            var walk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(walk == null)
            {
                return null;
            }
            context.Walks.Remove(walk);
            await context.SaveChangesAsync();
            return walk;
        }
         
        public async Task<Walk> Update(Guid id, Walk Walk)
        {
            var walkDB = await context.Walks.Include(x=>x.Region).Include(x=>x.WalkDifficulty).FirstOrDefaultAsync(x => x.Id == id);
            if(walkDB == null)
            {
                return null;
            }
            walkDB.Name = Walk.Name;
            walkDB.Length = Walk.Length;
            walkDB.RegionId = Walk.RegionId;
            walkDB.WalkDifficultyId = Walk.WalkDifficultyId;
            await context.SaveChangesAsync();
            return walkDB;
        }
    }
}
