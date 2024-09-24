namespace CL.Core.Shared.ModelViews
{
    public class NovoEndereco
    {
        /// <example>58040440</example>
        public int CEP { get; set; }
        /// <example>PB</example>
        public string Estado { get; set; }
        /// <example>João Pessoa</example>
        public string Cidade { get; set; }
        /// <example>Rua A</example>
        public string Logradouro { get; set; }
        /// <example>405</example>
        public string Numero { get; set; }
        /// <example>Ao lado do posto</example>
        public string Complemento { get; set; }
    }
}