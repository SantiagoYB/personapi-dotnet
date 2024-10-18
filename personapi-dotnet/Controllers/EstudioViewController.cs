using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    public class EstudioViewController : Controller
    {
        private readonly IEstudiosRepository _estudioRepository;

        public EstudioViewController(IEstudiosRepository estudioRepository)
        {
            _estudioRepository = estudioRepository;
        }

        // Lista de todos los estudios
        // /estudioView
        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllAsync();
            return View(estudios);
        }

        // Info de un estudio
        // /estudioView/Info/{ccPer}/{idProf}
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
                await _estudioRepository.AddEstudioAsync(estudio);
                return RedirectToAction(nameof(Index));
            }
            return View(estudio);
        }
    }
}
