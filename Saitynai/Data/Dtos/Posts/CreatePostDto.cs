using System.ComponentModel.DataAnnotations;

namespace Saitynai.Data.Dtos.Posts
{
    public record CreatePostDto([Required] string Name,[Required] string Description);
}
