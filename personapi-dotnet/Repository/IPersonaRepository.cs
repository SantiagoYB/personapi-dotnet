using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public interface IPersonaRepository
    {
        Task<Persona> GetPersonaByIdAsync(int cc);
        Task<IEnumerable<Persona>> GetAllAsync();
        Task AddPersonaAsync(Persona persona);
        Task UpdatePersonaAsync(Persona persona);
        Task DeletePersonaAsync(int cc);
    }
}
