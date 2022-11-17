

using AutoMapper;
using Saitynai.Data.Dtos.Auth;
using Saitynai.Data.Dtos.Categories;
using Saitynai.Data.Dtos.Comments;
using Saitynai.Data.Dtos.Posts;
using Saitynai.Data.Entities;

namespace Saitynai.Data
{
    public class DemoRestProfile : Profile
    {
        public DemoRestProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
            CreateMap<Post, PostDto>();

            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
            CreateMap<Comment, CommentDto>();

            CreateMap<User, UserDto>();
        }
    }
}
