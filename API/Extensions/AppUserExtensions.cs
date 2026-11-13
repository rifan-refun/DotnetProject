using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions
{
    public static class AppUserExtensions
    {
        public static UserDto ToDto (this AppUser user, ITokenService tokenService)
        {
            return new UserDto
            {
                Id = user.Id,
                Emails = user.Emails,
                UserName = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }
    }
}