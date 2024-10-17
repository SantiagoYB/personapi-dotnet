using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudioController : ControllerBase
    {
        private readonly IEstudiosRepository _estudiosRepository;

        public EstudioController(IEstudiosRepository estudiosRepository)
        {
            _estudiosRepository = estudiosRepository;
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
        public async Task<ActionResult> AddEstudio([FromBody] Estudio estudio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _estudiosRepository.AddEstudioAsync(estudio);
            return CreatedAtAction(nameof(GetEstudioById), new { ccPer = estudio.CcPer, idProf = estudio.IdProf }, estudio);
        }

        [HttpPut("{ccPer}/{idProf}")]
        public async Task<ActionResult> UpdateEstudio(int ccPer, int idProf, [FromBody] Estudio estudio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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