using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;


namespace personapi_dotnet.Controllers
{
    public class TelefonoViewController:Controller
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public TelefonoViewController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        //Lista de todos los telefonos
        // /telefonoView
        public async Task<IActionResult> Index()
        {
            var telefonos = await _telefonoRepository.GetAllAsync();
            return View(telefonos);
        }

        //Info de un telefono
        // /telefonoView/Info/{id}
        public async Task<IActionResult> Info(int? id)
        {
            var telefono = await _telefonoRepository.GetTelefonoByNumberAsync(id.Value.ToString());
            return View(telefono);
        }

        // Acción GET para editar un teléfono
        public async Task<IActionResult> Edit(string numero)
        {
            var telefono = await _telefonoRepository.GetTelefonoByNumberAsync(numero);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // Acción POST para editar un teléfono
        [HttpPost]
        public async Task<IActionResult> Edit(Telefono telefono)
        {
            if (!ModelState.IsValid)
            {
                return View(telefono);
            }

            await _telefonoRepository.UpdateTelefonoAsync(telefono);
            return RedirectToAction(nameof(Index));
        }


        // Acción GET para mostrar la vista de confirmación de eliminación
        public async Task<IActionResult> Delete(string numero)
        {
            var telefono = await _telefonoRepository.GetTelefonoByNumberAsync(numero);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // Acción POST para confirmar la eliminación
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string numero)
        {
            await _telefonoRepository.DeleteTelefonoAsync(numero);
            return RedirectToAction(nameof(Index));
        }


        //Crear un telefono
        // /telefonoView/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Telefono telefono)
        {
            await _telefonoRepository.AddTelefonoAsync(telefono);
            return RedirectToAction(nameof(Index));
        }
    }
}
