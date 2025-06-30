using DTO.Produtos;

namespace UseCases.Produtos;

public interface IProdutoUseCase
{
    Task<ResultadoUnico<ProdutoDTO>> InserirProduto(RegistrarProdutoDTO produto);

    Task<List<ProdutoDTO>> ObterProdutosAsync(string complementoUrl);
}