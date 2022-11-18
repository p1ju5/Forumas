using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynai.Auth.Model;
using Saitynai.Data.Dtos.Comments;
using Saitynai.Data.Dtos.Posts;
using Saitynai.Data.Entities;
using Saitynai.Data.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Saitynai.Controllers
{
    [ApiController]
    [Route("api/categories/{categoryId}/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IAuthorizationService _authorizationService;

        public PostsController(IPostsRepository postsRepository, IMapper mapper, 
            ICategoriesRepository categoriesRepository, IAuthorizationService authorizationService)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
            _authorizationService = authorizationService;
        }
        [HttpGet]
        public async Task<ActionResult<PostDto>> GetAllAsync(int categoryId)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            var posts = await _postsRepository.GetAsync(categoryId);

            return Ok(posts.Select(o => _mapper.Map<PostDto>(o)));
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostDto>> GetAsync(int categoryId, int postId)
        {
            var category = await _postsRepository.GetAsync(categoryId, postId);
            if (category == null) return NotFound();

            return Ok(_mapper.Map<PostDto>(category));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<PostDto>> PostAsync(int categoryId, CreatePostDto postDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            var post = _mapper.Map<Post>(postDto);
            post.CategoryId = categoryId;
            post.UserId = User.FindFirst(CustomClaims.UserId).Value;

            await _postsRepository.InsertAsync(post);

            return Created($"/api/categories/{categoryId}/posts/{post.Id}", _mapper.Map<PostDto>(post));
        }

        [HttpPut("{postId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<PostDto>> PostAsync(int categoryId, int postId, UpdatePostDto postDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            var oldPost = await _postsRepository.GetAsync(categoryId, postId);
            if (oldPost == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, oldPost, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(postDto, oldPost);

            await _postsRepository.UpdateAsync(oldPost);

            return Ok(_mapper.Map<PostDto>(oldPost));
        }

        [HttpDelete("{postId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult> DeleteAsync(int categoryId, int postId)
        {
            var post = await _postsRepository.GetAsync(categoryId, postId);
            if (post == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            await _postsRepository.DeleteAsync(post);

            return NoContent();
        }
    }
}
