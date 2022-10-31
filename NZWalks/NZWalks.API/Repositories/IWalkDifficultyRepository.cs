using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>>  GetAll();
        Task<WalkDifficulty> Get(Guid id);
        Task<WalkDifficulty> Add(WalkDifficulty wd);
        Task<WalkDifficulty> Delete(Guid id);
        Task<WalkDifficulty> Update(Guid id, WalkDifficulty wd);

    }
}
