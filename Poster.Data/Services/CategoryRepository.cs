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
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(PosterContext context)
        {
            this.context = context;
        }

        private readonly PosterContext context;

        public async Task<IEnumerable<PostCategory>> GetPostCategoriesAsync()
        {
            List<PostCategory> categories = await context.PostCategories.Include(c => c.Posts).AsNoTracking().ToListAsync();

            foreach (var category in categories)
            {
                foreach (var post in category.Posts)
                {
                    post.PostCategory = null;
                }
            }

            return categories;
        }

        public async Task<PostCategory> GetCategoryByIdAsync(long categoryId)
        {
            PostCategory category =  await context.PostCategories.Include(c => c.Posts).AsNoTracking().FirstAsync(c => c.Id == categoryId);

            foreach (var post in category.Posts)
            {
                post.PostCategory = null;
            }

            return category;
        }


        public async Task CreateCategoryAsync(PostCategory category)
        {
            await context.PostCategories.AddAsync(category);

            await context.SaveChangesAsync();
        }
    }
}
