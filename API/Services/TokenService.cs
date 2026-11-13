using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user) 
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Token key not found in configuration") ; // mengambil token key dari appsettings.json
        if (tokenKey.Length < 64 ) 
            throw new Exception("Token key must be at least 64 characters long");
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey)); // membuat symmetric security key dari token key
        var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Emails), // menambahkan claim email dari user
                new(ClaimTypes.NameIdentifier, user.Id.ToString()) // menambahkan claim id dari user
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // membuat signing credentials dari key dan algoritma HMAC SHA512
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // menambahkan claims ke token descriptor
                Expires = DateTime.Now.AddDays(7), // token akan expired dalam 7 hari
                SigningCredentials = creds // menambahkan signing credentials ke token descriptor
            };

            var tokenHandler = new JwtSecurityTokenHandler(); // membuat instance token handler
            var token = tokenHandler.CreateToken(tokenDescriptor); // membuat token dari token descriptor

            return tokenHandler.WriteToken(token); // membuat token dan mengembalikan token dalam bentuk string

    
    
    }
}

