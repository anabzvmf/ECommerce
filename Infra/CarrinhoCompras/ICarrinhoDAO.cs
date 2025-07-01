using Model.CarrinhoCompras;
using Model.Produtos;
using Microsoft.AspNetCore.Identity;

namespace Infra.CarrinhoCompras;

public interface ICarrinhoDAO : IBaseDAO<Carrinho>
{
    Task<Carrinho> ObterProdutosCarrinhoAsync(long id);
    Task InserirItemAsync(DTO.Carrinho.CarrinhoItemDTO item);
}