using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDBContext context;

        public WalkDifficultyRepository(NZWalksDBContext context)
        {
            this.context = context;
        }
        public async Task<WalkDifficulty> Add(WalkDifficulty wd)
        {
            wd.Id = new Guid();
            await context.WalkDifficulties.AddAsync(wd);
            await context.SaveChangesAsync();
            return wd;
        }

        public async Task<WalkDifficulty> Delete(Guid id)
        {
            var wd = await context.WalkDifficulties.FindAsync(id);
            if (wd == null)
            {
                return null;
            }
            context.WalkDifficulties.Remove(wd);
            await context.SaveChangesAsync();
            return wd;
        }

        public async Task<WalkDifficulty> Get(Guid id)
        {
            return await context.WalkDifficulties.FindAsync(id);
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAll()
        {
            return await context.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> Update(Guid id, WalkDifficulty wd)
        {
            var wdDB = await context.WalkDifficulties.FindAsync(id);
            if (wdDB == null)
            {
                return null;
            }
            wdDB.Code = wd.Code;
            await context.SaveChangesAsync();
            return wd;
        }
    }
}
