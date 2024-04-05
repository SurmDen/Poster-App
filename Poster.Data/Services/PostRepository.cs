using Microsoft.EntityFrameworkCore;
using Poster.Core.Models;
using Poster.Data.Entities;
using Poster.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Data.Services
{
    public class PostRepository : IPostRepository
    {
        public PostRepository(PosterContext context)
        {
            this.context = context;
        }

        private readonly PosterContext context;

        public async Task CreatePostAsync(Post post)
        {
            await context.Posts.AddAsync(post);

            await context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(long postId)
        {
            Post post = await context.Posts.FirstAsync(p => p.Id == postId);

            context.Posts.Remove(post);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            List<Post> posts = await context.Posts.Include(p => p.User).Include(p => p.PostCategory).AsNoTracking().ToListAsync();

            foreach (var post in posts)
            {
                post.User.Posts = null;

                post.User.Password = "";

                post.PostCategory.Posts = null;
            }

            return posts;
        }

        public async Task<Post> GetPostByIdAsync(long postId)
        {
            Post post =  await context.Posts.Include(p => p.User).Include(p => p.PostCategory).AsNoTracking().FirstAsync(p => p.Id == postId);

            post.User.Posts = null;

            post.User.Password = "";

            post.PostCategory.Posts = null;

            await Console.Out.WriteLineAsync(post.User.FullName);

            return post;
        }
    }
}
