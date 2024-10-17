using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public interface IEstudiosRepository
    {
        Task<IEnumerable<Estudio>> GetAllAsync();
        Task<Estudio?> GetEstudioByIdAsync(int ccPer, int idProf);
        Task AddEstudioAsync(Estudio estudio);
        Task UpdateEstudioAsync(Estudio estudio);
        Task DeleteEstudioAsync(int ccPer, int idProf);
    }
}
