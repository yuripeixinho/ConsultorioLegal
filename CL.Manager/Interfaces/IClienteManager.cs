using CL.Core.Domain;
using CL.Core.Shared.ModelViews;


namespace CL.Manager.Interfaces
{
    public interface IClienteManager
    {
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<Cliente> GetClienteAsync(int id);
        Task DeleteClienteAsync(int id);
        Task<Cliente> InsertClienteAsync(NovoCliente cliente);
        Task<Cliente> UpdateClienteAsync(AlteraCliente cliente);
    }
}
