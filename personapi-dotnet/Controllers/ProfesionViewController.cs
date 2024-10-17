using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    public class ProfesionViewController : Controller
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionViewController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        // Lista de todas las profesiones
        // /profesionView
        public async Task<IActionResult> Index()
        {
            var profesion = await _profesionRepository.GetAllAsync();
            return View(profesion);
        }

        // Info de una profesion 
        // /ProfesionView/Info/{id}
        public async Task<IActionResult> Info(int? id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id.Value);
            return View(profesion);
        }

        // Editar una profesion
        // ProfesionView/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id.Value);
            return View(profesion);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Profesion profesion)
        {
            await _profesionRepository.UpdateProfesionAsync(profesion);
            return RedirectToAction(nameof(Index));
        }

        // Eliminar una profesion
        // ProfesionView/Delete?id={id}
        public async Task<IActionResult> Delete(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            return View(profesion);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _profesionRepository.DeleteProfesionAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Crear una persona
        // ProfesionView/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Profesion profesion)
        {
            await _profesionRepository.AddProfesionAsync(profesion);
            return RedirectToAction(nameof(Index));
        }
    }
}
