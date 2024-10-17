using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<Persona> GetPersonaByIdAsync(int cc);
        Task AddPersonaAsync(Persona persona);
        Task UpdatePersonaAsync(Persona persona);
        Task DeletePersonaAsync(int cc);
    }
}
