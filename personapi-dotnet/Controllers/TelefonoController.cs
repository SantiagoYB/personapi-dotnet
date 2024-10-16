using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TelefonoController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public TelefonoController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetAllTelefonos()
        {
            var telefonos = await _telefonoRepository.GetAllAsync();
            return Ok(telefonos);
        }

        [HttpGet("{dueno}")]
        public async Task<ActionResult<Telefono>> GetTelefonoByDueno(int dueno)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(dueno);
            if (telefono == null)
            {
                return NotFound();
            }

            return Ok(telefono);
        }

        [HttpPost]
        public async Task<ActionResult> AddTelefono([FromBody] Telefono telefono)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _telefonoRepository.AddTelefonoAsync(telefono);
            return CreatedAtAction(nameof(GetTelefonoByDueno), new { dueno = telefono.Dueno }, telefono);
        }

        [HttpPut("{dueno}")]
        public async Task<ActionResult> UpdateTelefono(int dueno, [FromBody] Telefono telefono)
        {
            if (dueno != telefono.Dueno)
            {
                return BadRequest("El ID del dueño no coincide con el del teléfono.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _telefonoRepository.UpdateTelefonoAsync(telefono);
            return NoContent();
        }

        [HttpDelete("{dueno}")]
        public async Task<ActionResult> DeleteTelefono(int dueno)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(dueno);
            if (telefono == null)
            {
                return NotFound();
            }

            await _telefonoRepository.DeleteTelefonoAsync(dueno);
            return NoContent();
        }
    }
}
