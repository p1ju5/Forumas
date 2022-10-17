using Microsoft.EntityFrameworkCore;
using Saitynai.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Data.Repositories
{
    public interface IPostsRepository
    {
        Task<Post> GetAsync(int categoryId, int postId);
        Task<List<Post>> GetAsync(int categoryId);
        Task InsertAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(Post post);
    }

    public class PostsRepository : IPostsRepository
    {
        private readonly DemoRestContext _context;
        public PostsRepository(DemoRestContext demoRest)
        {
            _context = demoRest;
        }

        public async Task<Post> GetAsync(int categoryId, int postId)
        {
            return await _context.Posts.FirstOrDefaultAsync(o => o.CategoryId == categoryId && o.Id == postId);
        }
        public async Task<List<Post>> GetAsync(int categoryId)
        {
            return await _context.Posts.Where(o => o.CategoryId == categoryId).ToListAsync();
        }
        public async Task InsertAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

    }
}
