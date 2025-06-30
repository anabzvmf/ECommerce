using DTO.ControleAcessos;
using DTO.Pedidos;

namespace UseCases.ControleAcessos;

public interface IUsuarioService
{
    Task<UsuarioDTO?> ObterUsuarioLogadoAsync();
    Task<List<PedidoDTO>> ObterPedidosUsuarioAsync();
}
