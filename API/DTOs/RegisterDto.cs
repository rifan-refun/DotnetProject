using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public  string UserName {get; set;} = "";

        [Required]
        [EmailAddress]
        [MinLength(4)]
        public  string Emails {get; set;} =""; 

        [Required]
        public  string Password {get; set;} =""; 
    }
}

