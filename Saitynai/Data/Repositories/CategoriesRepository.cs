using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Saitynai.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Saitynai.Data.Repositories
{
    public interface ICategoriesRepository
    {
        Task<Category> GetAsync(int categoryId);
        Task<List<Category>> GetAsync();
        Task InsertAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DemoRestContext _context;

        public CategoriesRepository(DemoRestContext demoRest)
        {
            _context = demoRest;
        }

        public async Task<Category> GetAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(o => o.Id == categoryId);
        }
        public async Task<List<Category>> GetAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task InsertAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
