using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Core.Shared.ModelViews
{
    /// <summary>
    /// Objeto utilizado para inserção de um novo cliente
    /// </summary>
    public class NovoCliente
    {
        /// <summary>
        /// Nome do cliente
        /// </summary>
        ///  <example>João do Caminhão</example>
        public string Nome { get; set; }
        /// <summary>
        /// Data do nascimento do cliente
        /// <example>1980-01-01</example>
        /// </summary>
        public DateTime DataNascimento { get; set; }
        /// <summary>
        /// Sexo do cliente
        /// </summary>
        /// <example>M</example>
        public char Sexo { get; set; }
        /// <summary>
        /// Teelfone do cliente
        /// </summary>
        /// <example>83988108810</example>
        public string Telefone { get; set; }
        /// <summary>
        /// Documento do cliente: CNH, CPF ou RG
        /// </summary>
        /// <example>121234412312</example>
        public string Documento { get; set; }
    }
}
