using CL.Core.Domain;
using CL.Data.Context;
using CL.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CL.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        protected readonly ClContext _context;

        public ClienteRepository(ClContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes
                .Include(p => p.Endereco)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _context.Clientes
                .Include(p => p.Endereco)
                .SingleOrDefaultAsync(p => p.Id == id); 
        }

        public async Task<Cliente> InsertClienteAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            var clienteConsultado = await _context.Clientes.FindAsync(cliente.Id);

            if (clienteConsultado is null)
                return null;

            // fazer isso não é uma boa solução, já que se nossa entiade tiver mais de 20 campos, por exemplo, teríamos que fazer 20 linhas para atualizar. Em vez disso, usamos a solução Entry.
            //clienteConsultado.Nome = cliente.Nome;
            //clienteConsultado.DataNascimento = cliente.DataNascimento;

            _context.Entry(clienteConsultado).CurrentValues.SetValues(cliente); // Essencialmente, este código copia todos os valores das propriedades da entidade cliente para clienteConsultado, que já está sendo rastreado pelo contexto.Após isso, o Entity Framework irá considerar que clienteConsultado foi modificado, e quando você chamar SaveChanges(), essas alterações serão persistidas no banco de dados.

            //_context.Clientes.Update(clienteConsultado); // não é necessário fazer o update, pois na linha 51 está fazendo esse trabalho no "SetValues"
            await _context.SaveChangesAsync();
            return clienteConsultado;
        }

        public async Task DeleteClienteAsync(int id)
        {
            var clienteConsultado = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clienteConsultado);
            await _context.SaveChangesAsync();
        }
    }
}
