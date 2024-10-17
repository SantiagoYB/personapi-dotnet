using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : Controller
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(IPersonaRepository personaRepository)
        {
            _personaRepository = _personaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personas = await _personaRepository.GetAllAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int cc, string nombre, string apellido, string genero, int edad)
        {
            var persona = new Persona
            {
                Cc = cc,
                Nombre = nombre,
                Apellido = apellido,
                Genero = genero,
                Edad = edad,
            };
            await _personaRepository.AddPersonaAsync(persona);
            return Ok(persona);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePersona(int id, [FromBody] Persona persona)
        {
            var personaUpdate = await _personaRepository.GetPersonaByIdAsync(id);
            await _personaRepository.UpdatePersonaAsync(personaUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personaRepository.DeletePersonaAsync(id);
            return NoContent();
        }
    }
}
