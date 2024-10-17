using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
namespace personapi_dotnet.Repository
{
    public class EstudiosRepository : IEstudiosRepository
    {
        private readonly PersonaDbContext _context;

        public EstudiosRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudio>> GetAllAsync()
        {
            return await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .ToListAsync();
        }

        public async Task<Estudio?> GetEstudioByIdAsync(int ccPer, int idProf)
        {
            return await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(e => e.CcPer == ccPer && e.IdProf == idProf);
        }

        public async Task AddEstudioAsync(Estudio estudio)
        {
            await _context.Estudios.AddAsync(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEstudioAsync(Estudio estudio)
        {
            _context.Estudios.Update(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstudioAsync(int ccPer, int idProf)
        {
            var estudio = await _context.Estudios
                .FirstOrDefaultAsync(e => e.CcPer == ccPer && e.IdProf == idProf);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
