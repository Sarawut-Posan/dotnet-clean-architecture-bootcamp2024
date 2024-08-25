using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositories {
    public class CategoryRepository : ICategoryRepository {
        
        protected readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       public async Task<List<Category>> GetAllCategories()
        {
            return await dbContext.Categories.AsNoTracking().ToListAsync();
        }
        public async Task<Category> GetByIdAsync(Guid id) 
        {
            return await dbContext.Categories.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.Where(f => f.Id == category.Id).FirstOrDefaultAsync();
            
            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;
            existingCategory.UrlHandle = category.UrlHandle;

            await dbContext.SaveChangesAsync();

            return existingCategory;
        }
         
        public async Task<Category> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.Where(f => f.Id == id).FirstOrDefaultAsync();
            
            if (existingCategory == null)
            {
                return null;
            }

            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();

            return existingCategory;
        }
       
    }
}
