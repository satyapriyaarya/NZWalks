using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>>  GetAll();
        Task<Region> Get(Guid id);
        Task<Region> Add(Region region);
        Task<Region> Delete(Guid id);
        Task<Region> Update(Guid id, Region region);

    }
}
