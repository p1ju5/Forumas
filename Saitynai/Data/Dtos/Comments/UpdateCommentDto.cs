using System.ComponentModel.DataAnnotations;
using System;

namespace Saitynai.Data.Dtos.Comments
{
    public record UpdateCommentDto([Required] string Description);
}
