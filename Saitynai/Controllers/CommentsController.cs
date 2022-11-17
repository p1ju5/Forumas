using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynai.Data.Dtos.Categories;
using Saitynai.Data.Dtos.Posts;
using Saitynai.Data.Dtos.Comments;
using Saitynai.Data.Entities;
using Saitynai.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saitynai.Auth.Model;

namespace Saitynai.Controllers
{
    [ApiController]
    [Route("api/categories/{categoryId}/posts/{postId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public CommentsController(ICategoriesRepository categoriesRepository, IPostsRepository postsRepository, 
            ICommentsRepository commentsRepository, IMapper mapper, IAuthorizationService authorizationService)
        {
            _categoriesRepository = categoriesRepository;
            _postsRepository = postsRepository;
            _commentsRepository = commentsRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<CommentDto>> GetAllAsync(int categoryId, int postId)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            var post = await _postsRepository.GetAsync(categoryId, postId);
            if (post == null) return NotFound();

            var comments = await _commentsRepository.GetAsync(postId);

            return Ok(comments.Select(o => _mapper.Map<CommentDto>(o)));

        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentDto>> GetAsync(int categoryId, int postId, int commentId)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            var post = await _postsRepository.GetAsync(categoryId, postId);
            if (post == null) return NotFound();

            var comment = await _commentsRepository.GetAsync(postId, commentId);
            if (comment == null) return NotFound();

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<CommentDto>> PostAsync(int categoryId, int postId, CreateCommentDto commentDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            var post = await _postsRepository.GetAsync(categoryId, postId);
            if (post == null) return NotFound();

            var comment = _mapper.Map<Comment>(commentDto);
            comment.PostId = postId;
            post.UserId = User.FindFirst(CustomClaims.UserId).Value;

            await _commentsRepository.InsertAsync(comment);

            return Created($"/api/categories/{categoryId}/posts/{postId}/comments/{comment.Id}", _mapper.Map<CommentDto>(comment));
        }

        [HttpPut("{commentId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<CommentDto>> PostAsync(int categoryId, int postId, int commentId, UpdateCommentDto commentDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            var post = await _postsRepository.GetAsync(categoryId, postId);
            if (post == null) return NotFound();

            var oldComment = await _commentsRepository.GetAsync(postId, commentId);
            if (oldComment == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, oldComment, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return NotFound();
            }

            _mapper.Map(commentDto, oldComment);

            await _commentsRepository.UpdateAsync(oldComment);

            return Ok(_mapper.Map<CommentDto>(oldComment));
        }

        [HttpDelete("{commentId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult> DeleteAsync(int categoryId, int postId, int commentId)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            var post = await _postsRepository.GetAsync(categoryId, postId);
            if (post == null) return NotFound();

            var comment = await _commentsRepository.GetAsync(postId, commentId);
            if (comment == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, comment, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return NotFound();
            }

            await _commentsRepository.DeleteAsync(comment);

            return NoContent();
        }

    }

}
