using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using System.Security.Cryptography;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text;
using API.Interfaces;
using API.Extensions;

//account controller untuk mengatur route API yang berhubungan dengan account, seperti register dan login
namespace API.Controllers
{
    public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
    {

        [HttpPost("register")] //api/account/register
       public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await EmailExists(registerDto.Emails)) return BadRequest("Email is taken");
            
            using var hmac = new HMACSHA512(); //pakai using untuk memastikan hmac di dispose/dibuang setelah selesai digunakan, 
            //karena HMACSHA512 mengimplementasikan IDisposable
            var user = new AppUser
            {
                Emails = registerDto.Emails.ToLower(),
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user.ToDto(tokenService);
        }

        [HttpPost("login")] //api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) 
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Emails == loginDto.Emails);
            if (user == null) return Unauthorized("Invalid email");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return user.ToDto(tokenService);
        }

        private async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Emails == email.ToLower());
        }

       
       
    }
}