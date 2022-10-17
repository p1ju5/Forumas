using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Saitynai.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Saitynai.Data.Repositories
{
    public interface ICommentsRepository
    {
        Task<Comment> GetAsync(int postId, int commentId);
        Task<List<Comment>> GetAsync(int postId);
        Task InsertAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Comment comment);
    }

    public class CommentsRepository : ICommentsRepository
    {
        private readonly DemoRestContext _context;

        public CommentsRepository(DemoRestContext demoRest)
        {
            _context = demoRest;
        }

        public async Task<Comment> GetAsync(int postId, int commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(o => o.PostId == postId && o.Id == commentId);
        }
        public async Task<List<Comment>> GetAsync(int postId)
        {
            return await _context.Comments.Where(o => o.PostId == postId).ToListAsync();
        }
        public async Task InsertAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
