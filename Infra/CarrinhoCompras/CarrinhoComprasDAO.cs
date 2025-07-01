using Model.CarrinhoCompras;

namespace Infra.CarrinhoCompras;

public class CarrinhoComprasDAO : BaseDAO<Carrinho>, ICarrinhoDAO
{
    protected override string NomeTabela => "carrinhoCompras";

    public async Task<Carrinho> ObterProdutosCarrinhoAsync(long id)
    {
        string sql = $"SELECT * FROM carrinhoCompras where id = @id";

        var result = await SelecionarAsync<ItemCarrinho>(sql);

        Carrinho c = new Carrinho();

        foreach (var item in result)
        {
            c.Itens.Add(item);
        }

        return c;
    }

}