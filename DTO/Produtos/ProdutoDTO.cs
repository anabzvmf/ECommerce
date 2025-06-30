namespace DTO.Produtos;

public record struct ProdutoDTO
(
    long Id,
    string Nome,
    string Descricao,
    int Preco,
    int Estoque,
    string? ImagemUrl,
    DateTime DataCadastro
)
{
}
