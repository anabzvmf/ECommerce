namespace DTO.Produtos
{
    public class RegistrarProdutoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Preco { get; set; }
        public int Estoque { get; set; }

        public string ImagemUrl { get; set; } = string.Empty;
    }
}
