using CL.Core.Domain;
using CL.Core.Shared.ModelViews;
using CL.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SerilogTimings;
using SerilogTimings.Extensions;


namespace CL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteManager _clienteManager;
        private readonly ILogger<ClientesController> _logger;   

        public ClientesController(IClienteManager clienteManager, ILogger<ClientesController> logger)
        {
            _clienteManager = clienteManager;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados na base
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Cliente),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            throw new Exception("Erro de teste");

            return Ok(await _clienteManager.GetClientesAsync());
        }

        /// <summary>
        /// Retorna um cliente consultado pelo id
        /// </summary>
        /// <param name="id" example="123">Id do cliente.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _clienteManager.GetClienteAsync(id));
        }

        /// <summary>
        /// Insere um novo cliente
        /// </summary>
        /// <param name="novoCliente"></param>
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NovoCliente novoCliente)
        {
            _logger.LogInformation("Objeto recebido {@novoCliente}", novoCliente);

            Cliente clienteInserido;
            using (Operation.Time("Tempo de adição de um novo cliente."))
            {
                _logger.LogInformation("Foi requisitado a inserção de um novo cliente");
                clienteInserido = await _clienteManager.InsertClienteAsync(novoCliente);
             }

            return CreatedAtAction(nameof(Get), new { id = clienteInserido.Id }, clienteInserido);
        }

        /// <summary>
        /// Altera um ciente.
        /// </summary>
        /// <param name="alteraCliente"></param>
        [HttpPut]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] AlteraCliente alteraCliente)
        {
            var ClienteAtualizado = await _clienteManager.UpdateClienteAsync(alteraCliente);

            if (ClienteAtualizado == null)
                return NotFound();

            return Ok(ClienteAtualizado);
        }

        /// <summary>
        /// Exclui um cliente.
        /// </summary>
        /// <param name="id" example="123"></param>
        /// <remarks>Ao excluir um cliente, o mesmo será removido permanentemente da base</remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteManager.DeleteClienteAsync(id);

            return NoContent();
        }
    }
}
