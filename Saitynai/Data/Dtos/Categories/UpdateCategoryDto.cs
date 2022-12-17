using System.ComponentModel.DataAnnotations;

namespace Saitynai.Data.Dtos.Categories
{
    public record UpdateCategoryDto([Required] string Name);
}
