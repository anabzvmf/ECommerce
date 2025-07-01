using Model.Produtos;
using DTO.Produtos;

namespace Infra.Produtos;

public class ProdutosDAO : BaseDAO<Produto>, IProdutoDAO
{
    protected override string NomeTabela => "produto";

    public async Task Ocultar(long id)
    {
        string sql = $"UPDATE {NomeTabela}" +
            $" SET oculto = 1" +
            " WHERE " +
            " id = @Id";

        await ExecutarAsync(sql, new { Id = id });
    }

    public virtual async Task<IEnumerable<Produto>> RetornarComPaginacaoDescendenteAsync(long idAutor, long? ultimoIdConsultado, int numeroRegsASeremRetornados = 100)
    {
        var campos = "";

        if (ultimoIdConsultado == null)
            ultimoIdConsultado = long.MaxValue;

        foreach (var nomeProp in GetPropriedades(typeof(Produto)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT id as Id{campos}" +
            $" FROM {NomeTabela} WHERE idautor = @IdAutor AND oculto = 0 AND id < @Id ORDER BY id DESC" +
            $" LIMIT {numeroRegsASeremRetornados}";

        return await SelecionarAsync(sql, new { IdAutor = idAutor, Id = ultimoIdConsultado });
    }
    public async Task<List<Produto>> ObterProdutosAsync(string complementoUrl)
    {
        string sql = "SELECT * FROM produto";

        var result = await SelecionarAsync<Produto>(sql);

        var produto = result.Select(p => new Produto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            Estoque = p.Estoque,
            ImagemUrl = p.ImagemUrl,
            DataCadastro = p.DataCadastro
        }).ToList();

        return produto;
    }
    public async Task<Produto> ObterUnicoProdutoAsync(long id)
    {
        string sql = "SELECT * FROM produto WHERE id=@id";

        var result = await SelecionarUnicoAsync<Produto>(sql);

        return result;
    }

}