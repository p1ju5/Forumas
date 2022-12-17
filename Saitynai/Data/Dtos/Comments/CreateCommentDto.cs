using System;
using System.ComponentModel.DataAnnotations;

namespace Saitynai.Data.Dtos.Comments
{
    public record CreateCommentDto(int Id, [Required] string Description, DateTime CreatedDate, string UserId);
}
