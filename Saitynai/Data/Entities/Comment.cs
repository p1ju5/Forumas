using Saitynai.Auth.Model;
using Saitynai.Data.Dtos.Auth;
using System;
using System.ComponentModel.DataAnnotations;

namespace Saitynai.Data.Entities
{
    public class Comment : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
