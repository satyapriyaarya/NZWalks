using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>>  GetAll();
        Task<Walk> Get(Guid id);
        Task<Walk> Add(Walk walk);
        Task<Walk> Delete(Guid id);
        Task<Walk> Update(Guid id, Walk Walk);

    }
}
