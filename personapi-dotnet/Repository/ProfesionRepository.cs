using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public class ProfesionRepository : IProfesionRepository
    {
        private readonly PersonaDbContext _context;

        public ProfesionRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesion>> GetAllAsync()
        {
            return await _context.Profesiones.ToListAsync();
        }

        public async Task<Profesion?> GetProfesionByIdAsync(int id)
        {
            return await _context.Profesiones.FindAsync(id);
        }

        public async Task AddProfesionAsync(Profesion profesion)
        {
            await _context.Profesiones.AddAsync(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfesionAsync(Profesion profesion)
        {
            _context.Profesiones.Update(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfesionAsync(int id)
        {
            var profesion = await _context.Profesiones.FindAsync(id);
            if (profesion != null)
            {
                _context.Profesiones.Remove(profesion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
