namespace NZWalks.API.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //navigation
        public List<User_Role> UserRole { get; set; }
    }
}
