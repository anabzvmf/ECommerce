using Model.CarrinhoCompras;
using Model.Produtos;
using UseCases.CarrinhoCompras;
using Infra.CarrinhoCompras;

namespace Frontend.Helpers;
public class CarrinhoService : ICarrinhoService
{

    CarrinhoComprasDAO cDao = new CarrinhoComprasDAO();
    private static Carrinho carrinho = new Carrinho();

    public async Task AdicionarItemAsync(DTO.Carrinho.CarrinhoItemDTO itemDto)
    {
        // Persistir no banco
        await cDao.InserirItemAsync(itemDto);
    }

    
    public async Task<Carrinho> ObterCarrinhoAsync(long id)
    {
        Console.WriteLine($"[CarrinhoService] indo pegar");
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
