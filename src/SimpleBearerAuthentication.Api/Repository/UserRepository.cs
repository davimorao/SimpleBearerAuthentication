using Poc.JWT.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Poc.JWT.Repository
{
    public static class UserRepository
    {
        // simulates database access
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "davi", Password = "davi", Role = "manager" },
                new User { Id = 2, Username = "erika", Password = "erika", Role = "account" }
            };
            return 
                users.Where(x => 
                        x.Username.ToLower().Equals(username, System.StringComparison.OrdinalIgnoreCase) 
                        && x.Password.ToLower().Equals(password, System.StringComparison.OrdinalIgnoreCase)
                     )
                     .FirstOrDefault();
        }
    }
}