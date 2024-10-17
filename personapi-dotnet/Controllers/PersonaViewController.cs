using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    public class PersonaViewController : Controller
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaViewController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        // Lista de todas las personas
        // /personaView
        public async Task<IActionResult> Index()
        {
            var personas = await _personaRepository.GetAllAsync();
            return View(personas);
        }

        // Info de una persona 
        // /PersonaView/Info/{cedula}
        public async Task<IActionResult> Info(int? id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id.Value);
            return View(persona);
        }

        // Editar una persona
        // PersonaView/Edit/{cedula}
        public async Task<IActionResult> Edit(int? id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id.Value);
            return View(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int cc, Persona persona)
        {
            await _personaRepository.UpdatePersonaAsync(persona); 
            return RedirectToAction(nameof(Index));
        }

        // Eliminar una persona
        // PersonaView/Delete?cc={cedula}
        public async Task<IActionResult> Delete(int cc)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(cc);
            return View(persona);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int cc)
        {
            await _personaRepository.DeletePersonaAsync(cc);
            return RedirectToAction(nameof(Index));
        }

        // Crear una persona
        // PersonaView/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Persona persona)
        {
            await _personaRepository.AddPersonaAsync(persona);
            return RedirectToAction(nameof(Index));
        }
    }
}
