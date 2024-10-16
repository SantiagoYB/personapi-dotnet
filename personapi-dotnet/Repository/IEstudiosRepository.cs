using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public interface IEstudiosRepository
    {
        Task<Estudio> GetEstudioByIdAsync(int idProf);
        Task<IEnumerable<Estudio>> GetAllAsync();
        Task AddEstudioAsync(Estudio estudio);
        Task UpdateEstudioAsync(Estudio estudio);
        Task DeleteEstudioAsync(int idProf);
    }
}
