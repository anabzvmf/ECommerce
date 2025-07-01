using Model.CarrinhoCompras;
using Model.Produtos;

namespace Frontend.Helpers;
public class CartService
{
    private Carrinho carrinho;

    public CartService()
    {
        carrinho = new Carrinho();
    }

    public Carrinho ObterCarrinho()
    {
        return carrinho;
    }

    public void AdicionarItemAoCarrinho(Produto produto, int quantidade)
    {
        carrinho.addItem(produto, quantidade);
    }

    public void RemoverItemDoCarrinho(int produtoId)
    {
        carrinho.RemoverItem(produtoId);
    }

    public void LimparCarrinho()
    {
        carrinho.LimparCarrinho();
    }
}
