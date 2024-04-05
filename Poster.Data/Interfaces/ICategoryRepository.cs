using Poster.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Data.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<PostCategory>> GetPostCategoriesAsync();

        public Task<PostCategory> GetCategoryByIdAsync(long categoryId);

        public Task CreateCategoryAsync(PostCategory category);
    }
}
