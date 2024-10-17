using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAllPersonas()
        {
            var personas = await _personaRepository.GetAllAsync();
            return Ok(personas);
        }

        [HttpGet("{cc}")]
        public async Task<ActionResult<Persona>> GetPersonaById(int cc)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(cc);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult> AddPersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _personaRepository.AddPersonaAsync(persona);
            return CreatedAtAction(nameof(GetPersonaById), new { cc = persona.Cc }, persona);
        }

        [HttpPut("{cc}")]
        public async Task<ActionResult> UpdatePersona(int cc, [FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _personaRepository.UpdatePersonaAsync(persona);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersona(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            await _personaRepository.DeletePersonaAsync(id);
            return NoContent();
        }
    }
}
