
using Model.CarrinhoCompras;
using Model.Produtos;
using System.Threading.Tasks;

namespace UseCases.CarrinhoCompras;

public interface ICarrinhoService
{
    Task AdicionarItemAsync(Produto produto);
    Task<Carrinho> ObterCarrinhoAsync(long id);

    Task RemoverDoCarrinhoAsync(long id);
}
