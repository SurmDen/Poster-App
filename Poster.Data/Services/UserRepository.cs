using Microsoft.EntityFrameworkCore;
using Poster.Core.Models;
using Poster.Data.Entities;
using Poster.Data.Interfaces;
using Poster.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Data.Services
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(PosterContext context)
        {
            this.context = context;
        }

        private readonly PosterContext context;

        public async Task CreateUserAsync(User user)
        {
            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(long userId)
        {
            User user = await context.Users.FirstAsync(u => u.Id == userId);

            context.Users.Remove(user);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            List<User> users = await context.Users.Include(u => u.Posts).AsNoTracking().ToListAsync();

            foreach (var user in users)
            {
                user.Password = "";

                foreach (var post in user.Posts)
                {
                    post.User = null;
                }
            }

            return users;
        }

        public async Task<User> GetUserByIdAsync(long userId)
        {
            User user = await context.Users.Include(u => u.Posts).AsNoTracking().FirstAsync(u => u.Id == userId);

            user.Password = "";

            foreach (var post in user.Posts)
            {
                post.User = null;
            }

            return user;
        }

        public async Task<User> GetUserByNameAndPasswordAsync(LoginModel loginModel)
        {
            User user =  await context.Users.Include(u => u.Posts).AsNoTracking().
                FirstAsync(u => u.FullName == loginModel.UserName && u.Password == loginModel.Password);

            user.Password = "";

            foreach (var post in user.Posts)
            {
                post.User = null;
            }

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            context.Users.Update(user);

            await context.SaveChangesAsync();
        }
    }
}
