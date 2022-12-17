using System;
using System.ComponentModel.DataAnnotations;

namespace Saitynai.Data.Dtos.Posts
{
    public record UpdatePostDto([Required] string Name, [Required] string Description);
}
