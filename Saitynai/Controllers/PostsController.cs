using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Saitynai.Data.Dtos.Posts;
using Saitynai.Data.Entities;
using Saitynai.Data.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Controllers
{
    [ApiController]
    [Route("api/categories/{categoryId}/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoriesRepository;

        public PostsController(IPostsRepository postsRepository, IMapper mapper, ICategoriesRepository categoriesRepository)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<PostDto>> GetAllAsync(int categoryId)
        {
            var categories = await _postsRepository.GetAsync(categoryId);
            return categories.Select(o => _mapper.Map<PostDto>(o));
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostDto>> GetAsync(int categoryId, int postId)
        {
            var category = await _postsRepository.GetAsync(categoryId, postId);
            if (category == null) return NotFound();

            return Ok(_mapper.Map<PostDto>(category));
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> PostAsync(int categoryId, CreatePostDto postDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound($"Couldn't find a category with id of {categoryId}");

            var post = _mapper.Map<Post>(postDto);
            post.CategoryId = categoryId;

            await _postsRepository.InsertAsync(post);

            return Created($"/api/categories/{categoryId}/posts/{post.Id}", _mapper.Map<PostDto>(post));
        }

        [HttpPut("{postId}")]
        public async Task<ActionResult<PostDto>> PostAsync(int categoryId, int postId, UpdatePostDto postDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound($"Couldn't find a category with id of {categoryId}");

            var oldPost = await _postsRepository.GetAsync(categoryId, postId);
            if (oldPost == null)
                return NotFound();

            _mapper.Map(postDto, oldPost);

            await _postsRepository.UpdateAsync(oldPost);

            return Ok(_mapper.Map<PostDto>(oldPost));
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult> DeleteAsync(int categoryId, int postId)
        {
            var post = await _postsRepository.GetAsync(categoryId, postId);
            if (post == null)
                return NotFound();

            await _postsRepository.DeleteAsync(post);

            return NoContent();
        }
    }
}
