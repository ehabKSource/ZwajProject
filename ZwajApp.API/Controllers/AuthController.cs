using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.DTOs;
using ZwajApp.API.Models;

namespace ZwajApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAthuRepository _athuRepository;
        private readonly IConfiguration _appSettings;

        public AuthController(IAthuRepository athuRepository , IConfiguration appSettings )
        {
            this._athuRepository = athuRepository;
            this._appSettings = appSettings;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult>  Register(UserRegisterDTO userRegisterDTO)
        {
            User user = new User();

            user.UserName = userRegisterDTO.UserName;

            if (await _athuRepository.UserExists(userRegisterDTO.UserName))
            {
                return BadRequest("Al ready exsits");
            }

            await _athuRepository.Register(user, userRegisterDTO.Password);

            return StatusCode(201);

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            User user = await _athuRepository.Login(loginDTO.UserName , loginDTO.Password);

            if (user == null) return Unauthorized();

            var tokenValue = generateJwtToken(user);


            return Ok(new{
                token=tokenValue
            });
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.GetValue<string>("AppSettings:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }






    }

}
