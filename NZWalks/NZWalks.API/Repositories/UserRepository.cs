using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;
        public UserRepository(NZWalksDBContext nZWalksDBContext)
        {
            this.nZWalksDBContext = nZWalksDBContext;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await nZWalksDBContext.Users.FirstOrDefaultAsync(
                x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                x.Password == password);
            if(user == null)
            {
                return null;
            }

            var userRoles = await nZWalksDBContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var item in userRoles)
                {
                    var role = await nZWalksDBContext.Roles.FirstOrDefaultAsync(x => x.Id == item.RoleId);
                    if(role!= null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}
