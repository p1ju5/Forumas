using System.ComponentModel.DataAnnotations;

namespace Saitynai.Data.Dtos.Categories
{
    public record CreateCategoryDto([Required] string Name);
}
