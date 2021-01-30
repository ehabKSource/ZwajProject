using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.DTOs;
using ZwajApp.API.Models;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IZwajRepository _repo;
        // Create a field to store the mapper object
        private readonly IMapper _mapper;

        public UsersController(IZwajRepository repo, IMapper mapper)
        {
            _repo = repo;

            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersToReturn = await _repo.GetUsers();

            var usersDto = _mapper.Map<IEnumerable<UserForDetailsDto>>(usersToReturn);

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getuser(int id)
        {
           var user = await _repo.GetUser(id);

           var userDto= _mapper.Map<UserForDetailsDto>(user);

            return Ok(userDto);
        }
    }
}
