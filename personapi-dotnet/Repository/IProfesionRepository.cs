using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public interface IProfesionRepository
    {
        Task<IEnumerable<Profesion>> GetAllAsync();
        Task<Profesion> GetProfesionByIdAsync(int id);
        Task AddProfesionAsync(Profesion profesion);
        Task UpdateProfesionAsync(Profesion profesion);
        Task DeleteProfesionAsync(int id);
    }
}
