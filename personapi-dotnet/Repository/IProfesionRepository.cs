using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public interface IProfesionRepository
    {
        Task<Profesion> GetProfesionByIdAsync(int id);
        Task<IEnumerable<Profesion>> GetAllAsync();
        Task AddProfesionAsync(Profesion profesion);
        Task UpdateProfesionAsync(Profesion profesion);
        Task DeleteProfesionAsync(int id);
    }
}
