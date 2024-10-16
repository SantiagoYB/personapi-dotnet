    using personapi_dotnet.Models.Entities;

    namespace personapi_dotnet.Repository
    {
        public interface ITelefonoRepository
        {
            Task<Telefono> GetTelefonoByIdAsync(int id);
            Task<IEnumerable<Telefono>> GetAllAsync();
            Task AddTelefonoAsync(Telefono telefono);
            Task UpdateTelefonoAsync(Telefono telefono);
            Task DeleteTelefonoAsync(int id);
        }
    }
