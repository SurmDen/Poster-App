using Poster.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Data.Interfaces
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> GetAllPostsAsync();

        public Task CreatePostAsync(Post post);

        public Task DeletePostAsync(long postId);

        public Task<Post> GetPostByIdAsync(long postId);
    }
}
