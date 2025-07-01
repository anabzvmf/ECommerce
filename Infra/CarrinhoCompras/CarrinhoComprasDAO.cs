using Model.CarrinhoCompras;
using Model.Produtos;
using DTO.Carrinho;
using Infra.Produtos;
namespace Infra.CarrinhoCompras
{
    public class CarrinhoComprasDAO : BaseDAO<Carrinho>, ICarrinhoDAO
    {
        protected override string NomeTabela => "carrinhoCompras";

        public async Task<Carrinho> ObterProdutosCarrinhoAsync(long usuarioId)
        {
            Console.WriteLine($"[DAO] indo pegar, id: {usuarioId}");

            ProdutosDAO pDao = new ProdutosDAO();

            string sql = @"
                SELECT c.produtoId, c.quantidade 
                FROM CarrinhoItem c 
                WHERE c.usuarioId = @usuarioId";

            var result = await SelecionarUnicoAsync<Carrinho>(sql, new { usuarioId });

            Console.WriteLine($"Resultado: {result}");
            if (result == null || !result.Itens.Any())
                return new Carrinho();

            Carrinho carrinho = new Carrinho();

            foreach (var item in result.Itens)
            {
                var produto = await pDao.RetornarPorIdAsync(item.Id);

                ItemCarrinho itemCarrinho = new ItemCarrinho
                {
                    Produto = produto,
                    Quantidade = item.Quantidade
                };

                carrinho.Itens.Add(itemCarrinho);
            }

            return carrinho;
        }


        public async Task InserirItemAsync(DTO.Carrinho.CarrinhoItemDTO item)
        {
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