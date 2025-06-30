using System.Text.Json;
using Microsoft.JSInterop;
using DTO.ControleAcessos;
using DTO.Pedidos;
using UseCases.ControleAcessos;

namespace Frontend.Helpers;

public class UsuarioService : IUsuarioService
{
    private readonly IJSRuntime _js;
    public UsuarioService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<UsuarioDTO?> ObterUsuarioLogadoAsync()
    {
        var json = await _js.InvokeAsync<string>("localStorage.getItem", "user");
        if (string.IsNullOrEmpty(json)) return null;
        return JsonSerializer.Deserialize<UsuarioDTO>(json);
    }

    public Task<List<PedidoDTO>> ObterPedidosUsuarioAsync()
    {
        // Exemplo: retorna lista vazia. Implemente conforme sua lógica.
        return Task.FromResult(new List<PedidoDTO>());
    }
}
