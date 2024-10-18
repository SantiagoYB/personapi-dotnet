using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    public class EstudioViewController : Controller
    {
        private readonly IEstudiosRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudioViewController(IEstudiosRepository estudioRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;  // Asegúrate de inicializarlo
            _profesionRepository = profesionRepository;  // Asegúrate de inicializarlo
        }

        // Lista de todos los estudios
        // /estudioView
        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllAsync();
            return View(estudios);
        }

        // Acción para mostrar la información de un estudio específico
        // /EstudioView/Info/{ccPer}/{idProf}
        public async Task<IActionResult> Info(int ccPer, int idProf)
        {
            var estudio = await _estudioRepository.GetEstudioByIdAsync(ccPer, idProf);
            if (estudio == null)
            {
                return NotFound();
            }
            return View(estudio);
        }

        // Editar un estudio
        // /estudioView/Edit/{ccPer}/{idProf}
        public async Task<IActionResult> Edit(int ccPer, int idProf)
        {
            var estudio = await _estudioRepository.GetEstudioByIdAsync(ccPer, idProf);
            if (estudio == null)
            {
                return NotFound();
            }
            return View(estudio);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int ccPer, int idProf, Estudio estudio)
        {
            if (ccPer != estudio.CcPer || idProf != estudio.IdProf)
            {
                return BadRequest();
            }
            await _estudioRepository.UpdateEstudioAsync(estudio);
            return RedirectToAction(nameof(Index));
        }

        // Eliminar un estudio
        // /estudioView/Delete?ccPer={ccPer}&idProf={idProf}
        public async Task<IActionResult> Delete(int ccPer, int idProf)
        {
            var estudio = await _estudioRepository.GetEstudioByIdAsync(ccPer, idProf);
            if (estudio == null)
            {
                return NotFound();
            }
            return View(estudio);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int ccPer, int idProf)
        {
            await _estudioRepository.DeleteEstudioAsync(ccPer, idProf);
            return RedirectToAction(nameof(Index));
        }

        // Crear un estudio
        // /estudioView/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                // Validar si CcPer tiene un valor válido
                if (estudio.CcPer == 0)
                {
                    ModelState.AddModelError("", "El campo Cédula de Persona es requerido.");
                    return View(estudio);
                }

                // Buscar la persona basada en el identificador
                estudio.CcPerNavigation = await _personaRepository.GetPersonaByIdAsync(estudio.CcPer);

                // Validar que se encontró la entidad relacionada
                if (estudio.CcPerNavigation == null)
                {
                    ModelState.AddModelError("", "La persona no fue encontrada.");
                    return View(estudio);
                }

                // Buscar la profesión de la misma manera
                estudio.IdProfNavigation = await _profesionRepository.GetProfesionByIdAsync(estudio.IdProf);
                if (estudio.IdProfNavigation == null)
                {
                    ModelState.AddModelError("", "La profesión no fue encontrada.");
                    return View(estudio);
                }

                // Si todo es válido, guardar el estudio
                await _estudioRepository.AddEstudioAsync(estudio);
                return RedirectToAction(nameof(Index));
            }

            // Si hay algún error en la validación, regresar la vista con los errores
            return View(estudio);
        }




    }
}
