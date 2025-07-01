using Model.CarrinhoCompras;
using Model.Produtos;
using System.Linq;
using System.Threading.Tasks;
using Infra.CarrinhoCompras;

namespace UseCases.CarrinhoCompras;

public class CarrinhoService : ICarrinhoService
{
    CarrinhoComprasDAO cDao = new CarrinhoComprasDAO();
    private static Carrinho carrinho = new Carrinho();

    public async Task AdicionarItemAsync(Produto produto)
    {
        var itemExistente = carrinho.Itens.FirstOrDefault(x => x.Produto.Id == produto.Id);

        if (itemExistente != null)
        {
            itemExistente.Quantidade++;
        }
        else
        {
            var novoItem = new ItemCarrinho
            {
                Produto = produto,
                Quantidade = 1
            };
            carrinho.Itens.Add(novoItem);
        }

        await Task.CompletedTask;
    }

    
    public async Task<Carrinho> ObterCarrinho(long id)
    {
        return await cDao.ObterProdutosCarrinhoAsync(id);
    }

    public async Task RemoverDoCarrinhoAsync(long produtoId)
    {
        var itemParaRemover = carrinho.Itens.FirstOrDefault(p => p.Produto.Id == produtoId);

        if (itemParaRemover != null)
        {
            carrinho.Itens.Remove(itemParaRemover);
        }

        await Task.CompletedTask;
    }
}

