using Model.CarrinhoCompras;

using Model.CarrinhoCompras;

namespace Infra.CarrinhoCompras
{
    public class CarrinhoComprasDAO : BaseDAO<Carrinho>, ICarrinhoDAO
    {
        protected override string NomeTabela => "carrinhoCompras";

        public async Task<Carrinho> ObterProdutosCarrinhoAsync(long id)
        {
            string sql = $"SELECT * FROM carrinhoCompras where id = @id";
            var result = await SelecionarAsync<ItemCarrinho>(sql, new { id });
            Carrinho c = new Carrinho();
            foreach (var item in result)
            {
                c.Itens.Add(item);
            }
            return c;
        }

        public async Task InserirItemAsync(DTO.Carrinho.CarrinhoItemDTO item)
        {
            // Supondo que a tabela tenha colunas: UsuarioId, ProdutoId, Quantidade
            string sql = "INSERT INTO CarrinhoItem (UsuarioId, ProdutoId, Quantidade) VALUES (@UsuarioId, @ProdutoId, @Quantidade)";
            var parametros = new
            {
                UsuarioId = item.UsuarioId,
                ProdutoId = item.ProdutoId,
                Quantidade = item.Quantidade
            };
            await ExecutarAsync(sql, parametros);
        }
    }
}