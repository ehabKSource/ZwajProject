using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZwajApp.API.DTOs
{
    public class UserRegisterDTO 
    {   [Required]
        public string  UserName { get; set; }

        [Required]
        public string Password  { get; set; }
    }
}
