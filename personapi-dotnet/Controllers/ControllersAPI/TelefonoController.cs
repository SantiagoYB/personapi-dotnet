using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonoController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IPersonaRepository _personaRepository;

        public TelefonoController(ITelefonoRepository telefonoRepository, IPersonaRepository personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetAllTelefonos()
        {
            var telefonos = await _telefonoRepository.GetAllAsync();
            return Ok(telefonos);
        }

        [HttpGet("{numero}")]
        public async Task<ActionResult<Telefono>> GetTelefonoByDueno(string numero)
        {
            var telefono = await _telefonoRepository.GetTelefonoByNumberAsync(numero);
            if (telefono == null)
            {
                return NotFound();
            }

            return Ok(telefono);
        }

        [HttpPost]
        public async Task<ActionResult> AddTelefono(string numero, string operador, int duenio)
        {
            var newTelf = new Telefono
            {
                Num = numero,
                Oper = operador,
                Dueno = duenio,
                DuenoNavigation = await _personaRepository.GetPersonaByIdAsync(duenio)
            };

            await _telefonoRepository.AddTelefonoAsync(newTelf);
            return CreatedAtAction(nameof(GetTelefonoByDueno), new { dueno = newTelf.Dueno }, newTelf);
        }

        [HttpPut("{dueno}")]
        public async Task<ActionResult> UpdateTelefono(int dueno, [FromBody] Telefono telefono)
        {
            await _telefonoRepository.UpdateTelefonoAsync(telefono);
            return NoContent();
        }

        [HttpDelete("{numero}")]
        public async Task<ActionResult> DeleteTelefono(string numero)
        {
            var telefono = await _telefonoRepository.GetTelefonoByNumberAsync(numero);
            if (telefono == null)
            {
                return NotFound();
            }

            await _telefonoRepository.DeleteTelefonoAsync(numero);
            return NoContent();
        }
    }
}