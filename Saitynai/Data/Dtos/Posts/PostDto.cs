using System;

namespace Saitynai.Data.Dtos.Posts
{
    public record PostDto(int Id, string Name, string Description, DateTime CreatedDate, string UserId);
}
