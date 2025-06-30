using DTO.Carrinho;
using System.Threading.Tasks;

namespace UseCases.Carrinho;

public interface ICarrinhoService
{
    Task AdicionarItemAsync(CarrinhoDTO carrinho);
    Task<CarrinhoDTO> ObterCarrinhoAsync();
}
