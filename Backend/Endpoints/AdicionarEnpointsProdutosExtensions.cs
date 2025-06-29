using DTO.Produtos;
using Model.Produtos;
using UseCases;
using UseCases.ControleAcessos;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Backend.Endpoints;

public static void AdicionarEnpointsUsuariosExtensions
{
    <summary>
    </summary>
    <param name="app">Instância do WebApplication.</param>
    public static void AdicionarEndpointsProdutos(this WebApplication app)
    {
        var usuarios = app.MapGroup("/produtos")
            .WithTags("Produtos")
            .WithDescription("Endpoints relacionados a produtos");
        usuarios.MapGet("/", RetornarProdutos)
            .WithName("Retornar Produtos")
            .WithSummary("Retorna os Produtos");

        usuarios.MapPost("/", InserirProdutos)
            .WithName("Inserir produtos")
            .WithSummary("Insere um novo produto na base de dados");

        usuarios.MapPut("/{id}", AlterarProduto)
            .WithName("Alterar produto")
            .WithSummary("Altera um produto na base de dados")
            .RequireAuthorization();
    }
    private static async Task<IResult> InserirUsuario(RegistrarProdutoDTO usuario, IProdutoService produtoService)
    {
        try
        {
            var resultado = await controleAcessoUseCase.RegistrarUsuario(usuario);
            return resultado.Sucesso
                ? TypedResults.Created($"/{resultado.Objeto.Id}", resultado.Objeto)
                : TypedResults.BadRequest(resultado.Erros);
        }
        catch (Exception ex)
        {
#if DEBUG
            var metodo = MethodBase.GetCurrentMethod();

            if (metodo != null)
                Debug.WriteLine($"Exception in {metodo.Name}: {ex.Message}");
#endif

            return TypedResults.InternalServerError();
        }
    }

}