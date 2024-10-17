using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository
{
    public interface ITelefonoRepository
    {
        Task<IEnumerable<Telefono>> GetAllAsync();
        Task<Telefono> GetTelefonoByNumberAsync(string numero);
        Task AddTelefonoAsync(Telefono telefono);
        Task UpdateTelefonoAsync(Telefono telefono);
        Task DeleteTelefonoAsync(string numero);
    }
}
