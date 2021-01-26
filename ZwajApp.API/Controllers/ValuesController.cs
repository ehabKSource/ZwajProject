using Microsoft.AspNetCore.Mvc;
using Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }

        [AllowAnonymous]
        [HttpGet]
        public  IActionResult Get(){

            var values= _context.values.ToList();

            return Ok(values);

        }

        [HttpGet("{id}")]
        public  IActionResult Get(int id){

            var value=_context.values.FirstOrDefault(x=>x.Id==id);

            return Ok(value);

        }

    }
}