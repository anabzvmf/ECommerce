namespace DTO.Produtos;

public record struct ProdutoDTO
(
    long Id,
    string Nome,
    string Descricao,
    decimal Preco,
    int Estoque,
    string? ImagemUrl,
    DateTime DataCadastro,
    long CategoriaId
)
{
}
