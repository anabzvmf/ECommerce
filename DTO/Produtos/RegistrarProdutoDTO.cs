namespace DTO.Produtos;

public record struct RegistrarProdutoDTO
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
