using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDni.DataContext;
using WebApiDni.Models;

namespace WebApiDni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _db;
        public PersonController(PersonContext db)
        {
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)] //Bad Request
        public async Task<IActionResult> GetPerson()
        {
            var lista = await _db.Person.OrderBy(c => c.Nombre).ToListAsync();
            return Ok(lista);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)] //Error Interno
        public async Task<IActionResult> CrearPerson([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _db.AddAsync(person);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{Dni:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(404)] //Not found
        public async Task<IActionResult> GetPerson(string Dni)
        {
            var obj = await _db.Person.FirstOrDefaultAsync(c => c.Dni == Dni);

            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

    }
}
