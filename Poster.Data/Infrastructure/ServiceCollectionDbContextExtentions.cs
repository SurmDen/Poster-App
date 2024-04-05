using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Poster.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Data.Infrastructure
{
    public static class ServiceCollectionDbContextExtentions
    {
        public static void AddPosterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<PosterContext>(options =>
            {
                options.UseSqlite("Data Source = poster.db");
            }, ServiceLifetime.Transient);
        }
    }
}
