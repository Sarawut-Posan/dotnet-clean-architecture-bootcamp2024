using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(b => b.Categories).ToListAsync();
        }

        public Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            dbContext.BlogPosts.Add(blogPost);
            dbContext.SaveChangesAsync();
            return Task.FromResult(blogPost);
        }

        public async Task<BlogPost> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(b => b.Categories).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await dbContext.BlogPosts.Where(f => f.Id == blogPost.Id).FirstOrDefaultAsync();

            if (existingBlogPost == null)
            {
                return null;
            }

            existingBlogPost.Title = blogPost.Title;
            existingBlogPost.Content = blogPost.Content;
            existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            existingBlogPost.PublishedDate = blogPost.PublishedDate;
            existingBlogPost.UrlHandle = blogPost.UrlHandle;
            existingBlogPost.Author = blogPost.Author;
            existingBlogPost.Categories = blogPost.Categories;

            await dbContext.SaveChangesAsync();

            return existingBlogPost;
        }

        

    }
}
