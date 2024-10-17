using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudioController : ControllerBase
    {
        private readonly IEstudiosRepository _estudiosRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudioController(IEstudiosRepository estudiosRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository)
        {
            _estudiosRepository = estudiosRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudio>>> GetAllEstudios()
        {
            var estudios = await _estudiosRepository.GetAllAsync();
            return Ok(estudios);
        }

        [HttpGet("{ccPer}/{idProf}")]
        public async Task<ActionResult<Estudio>> GetEstudioById(int ccPer, int idProf)
        {
            var estudio = await _estudiosRepository.GetEstudioByIdAsync(ccPer, idProf);
            if (estudio == null)
            {
                return NotFound();
            }

            return Ok(estudio);
        }

        [HttpPost]
        public async Task<ActionResult> AddEstudio(int profesion, int cedula, DateOnly date, string universidad)
        {
            var newEstudio = new Estudio
            {
                IdProf = profesion,
                CcPer = cedula,
                Fecha = date,
                Univer = universidad,
                CcPerNavigation = await _personaRepository.GetPersonaByIdAsync(cedula),
                IdProfNavigation = await _profesionRepository.GetProfesionByIdAsync(profesion)
            };

            await _estudiosRepository.AddEstudioAsync(newEstudio);
            return CreatedAtAction(nameof(GetEstudioById), new { ccPer = newEstudio.CcPer, idProf = newEstudio.IdProf }, newEstudio);
        }

        [HttpPut("{ccPer}/{idProf}")]
        public async Task<ActionResult> UpdateEstudio(int ccPer, int idProf, [FromBody] Estudio estudio)
        {
            await _estudiosRepository.UpdateEstudioAsync(estudio);
            return NoContent();
        }

        [HttpDelete("{ccPer}/{idProf}")]
        public async Task<ActionResult> DeleteEstudio(int ccPer, int idProf)
        {
            var estudio = await _estudiosRepository.GetEstudioByIdAsync(ccPer, idProf);
            if (estudio == null)
            {
                return NotFound();
            }

            await _estudiosRepository.DeleteEstudioAsync(ccPer, idProf);
            return NoContent();
        }
    }
}