using Model.Produtos;
using Microsoft.AspNetCore.Identity;

namespace Infra.Produtos;

public interface IProdutoDAO : IBaseDAO<Produto>
{
    Task<IEnumerable<Produto>> RetornarComPaginacaoDescendenteAsync(long idProduto, long? ultimoIdConsultado, int numeroRegsASeremRetornados = 100);
    Task Ocultar(long id);
}