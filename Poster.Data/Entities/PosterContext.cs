using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poster.Core.Models;

namespace Poster.Data.Entities
{
    public class PosterContext : DbContext
    {
        public PosterContext(DbContextOptions<PosterContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostCategory> PostCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                FullName = "Denis",
                Password = "01052001denis",
                Email = "surmanidzedenis609@gmail.com"
            });

            modelBuilder.Entity<PostCategory>().HasData(new PostCategory[]
            {
                new PostCategory()
                {
                    CategoryName = "Спорт",
                    Id = 1
                },
                new PostCategory()
                {
                    CategoryName = "Политика",
                    Id = 2
                },
                new PostCategory()
                {
                    CategoryName = "Наука",
                    Id = 3
                },
                new PostCategory()
                {
                    CategoryName = "История",
                    Id = 4
                },
                new PostCategory()
                {
                    CategoryName = "Смешное",
                    Id = 5
                },
                new PostCategory()
                {
                    CategoryName = "Дом и быт",
                    Id = 6
                },
                new PostCategory()
                {
                    CategoryName = "Армия",
                    Id = 7
                },
            });
        }

    }
}
