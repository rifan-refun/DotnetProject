using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using System.Security.Cryptography;
using API.DTOs;


namespace API.Controllers
{
    public class AccountController(AppDbContext context) : BaseApiController
    {

        [HttpPost("register")] //api/account/register
       public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                Emails = registerDto.Emails,
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
       
    }
}