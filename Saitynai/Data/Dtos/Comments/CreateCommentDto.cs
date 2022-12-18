using System;
using System.ComponentModel.DataAnnotations;

namespace Saitynai.Data.Dtos.Comments
{
    public record CreateCommentDto([Required] string Description, string UserId);
}
