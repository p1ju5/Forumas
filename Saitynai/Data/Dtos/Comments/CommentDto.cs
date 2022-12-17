using System;

namespace Saitynai.Data.Dtos.Comments
{
    public record CommentDto(int Id, string Description, DateTime CreatedDate, string UserId);
}
