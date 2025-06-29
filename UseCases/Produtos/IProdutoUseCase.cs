using DTO.Produtos;

namespace UseCases.Produtos;

public interface IProdutoUseCase
{
    Task<ResultadoUnico<ProdutoDTO>> InserirProduto(ProdutoDTO produto);
}