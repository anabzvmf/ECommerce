
using Model.CarrinhoCompras;
using Model.Produtos;
using System.Threading.Tasks;

namespace UseCases.CarrinhoCompras;

public interface ICarrinhoService
{
    Task AdicionarItemAsync(DTO.Carrinho.CarrinhoItemDTO itemDto);
    Task<Carrinho> ObterCarrinhoAsync(long id);

    Task RemoverDoCarrinhoAsync(long id);
}
