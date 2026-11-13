using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string Emails { get; set; }

        public required string UserName { get; set; }

        public required string Token { get; set; }

        public string? ImageUrl { get; set; }
    }
}