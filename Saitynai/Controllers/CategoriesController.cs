using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynai.Auth.Model;
using Saitynai.Data.Dtos.Categories;
using Saitynai.Data.Dtos.Posts;
using Saitynai.Data.Entities;
using Saitynai.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public CategoriesController(ICategoriesRepository categoriesRepository, IMapper mapper, IAuthorizationService authorizationService)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoriesRepository.GetAsync();
            return categories.Select(o => _mapper.Map<CategoryDto>(o));
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetAsync(int categoryId)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound();

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CategoryDto>> PostAsync(CreateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            await _categoriesRepository.InsertAsync(category);

            return Created($"/api/categories/{category.Id}", _mapper.Map<CategoryDto>(category));
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CategoryDto>> PostAsync(int categoryId, UpdateCategoryDto categoryDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null) return NotFound($"Couldn't find a category with id of {categoryId}");

            _mapper.Map(categoryDto, category);

            await _categoriesRepository.UpdateAsync(category);

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteAsync(int categoryId)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);
            if (category == null)
                return NotFound();

            await _categoriesRepository.DeleteAsync(category);

            return NoContent();
        }

    }

}
