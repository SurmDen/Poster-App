using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poster.Data.Entities;
using Poster.Core.Models;
using Poster.Identity.Models;

namespace Poster.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();

        public Task CreateUserAsync(User user);

        public Task UpdateUserAsync(User user);

        public Task DeleteUserAsync(long userId);

        public Task<User> GetUserByIdAsync(long userId);

        public Task<User> GetUserByNameAndPasswordAsync(LoginModel loginModel);
    }
}
