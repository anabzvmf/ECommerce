namespace Backend.Endpoints;

public static class AdicionarEndpoints
{
    /// <summary>
    /// Método para adicionar todos os endpoints da API.
    /// </summary>
    /// <param name="app">Instância do WebApplication.</param>
    public static void AdicionarTodosEndpoints(this WebApplication app)
    {
        app.AdicionarEndpointsUsuarios();
        app.AdicionarEndpointsProdutos();
        app.AdicionarEndpointsCarrinho();
        // app.AdicionarEndpointsPedidos();
    }
}