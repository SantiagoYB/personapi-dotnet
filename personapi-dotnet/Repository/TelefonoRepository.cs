using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly PersonaDbContext _context;

        public TelefonoRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Telefono>> GetAllAsync()
        {
            return await _context.Telefonos.ToListAsync();
        }

        public async Task<Telefono?> GetTelefonoByNumberAsync(string numero)
        {
            return await _context.Telefonos.FirstOrDefaultAsync(t => t.Num == numero);
        }

        public async Task AddTelefonoAsync(Telefono telefono)
        {
            await _context.Telefonos.AddAsync(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTelefonoAsync(Telefono telefono)
        {
            _context.Telefonos.Update(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTelefonoAsync(string numero)
        {
            var telefono = await _context.Telefonos.FirstOrDefaultAsync(t => t.Num == numero);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }
    }
}
