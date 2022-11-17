using Microsoft.AspNetCore.Identity;
using System;

namespace Saitynai.Data.Dtos.Auth
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string AdditionalInfo { get; set; }
    }
}
