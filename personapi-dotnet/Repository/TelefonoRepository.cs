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

        public async Task<Telefono> GetTelefonoByIdAsync(int dueno)
        {
            return await _context.Telefonos.FirstOrDefaultAsync(t => t.Dueno == dueno);
        }

        public async Task AddTelefonoAsync(Telefono telefono)
        {
            _context.Telefonos.Add(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTelefonoAsync(Telefono telefono)
        {
            _context.Entry(telefono).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTelefonoAsync(int dueno)
        {
            var telefono = await _context.Telefonos.FirstOrDefaultAsync(t => t.Dueno == dueno);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }
    }
}
