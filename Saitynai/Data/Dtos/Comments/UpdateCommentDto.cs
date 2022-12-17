using System.ComponentModel.DataAnnotations;
using System;

namespace Saitynai.Data.Dtos.Comments
{
    public record UpdateCommentDto(int Id, [Required] string Description, DateTime CreatedDate, string UserId);
}
