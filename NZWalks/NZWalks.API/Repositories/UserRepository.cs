using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        //private readonly NZWalksDBContext nZWalksDBContext;
        private readonly List<User> users = new();
        public UserRepository()
        {
            users = new List<User> {
                new User{
                    FirstName="first1", LastName="last1", EmailAddress="email1", Id=Guid.NewGuid(), UserName="user1", Password="pass1" , Roles=new List<string>{"reader"}
                },
                new User{
                    FirstName="first2", LastName="last2", EmailAddress="email2", Id=Guid.NewGuid(), UserName="user2", Password="pass2", Roles=new List<string>{"reader", "writer" }
                }};
            //this.nZWalksDBContext = nZWalksDBContext;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            var user = users.FirstOrDefault(
                x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                x.Password == password);
            
            return user;
        }
    }
}
