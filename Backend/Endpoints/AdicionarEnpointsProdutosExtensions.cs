using DTO.Produtos;
using Model.Produtos;
using UseCases;
using UseCases.ControleAcessos;
using UseCases.Produtos; 
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Endpoints;

public static class AdicionarEnpointsProdutosExtensions
{
    /// <summary>
    /// Extension method to add product endpoints to the WebApplication
    /// </summary>
    /// <param name="app">Instância do WebApplication.</param>
    public static void AdicionarEndpointsProdutos(this WebApplication app)
    {
        var produtos = app.MapGroup("/produtos")
            .WithTags("Produtos")
            .WithDescription("Endpoints relacionados a produtos");
        produtos.MapGet("/", RetornarProdutos)
            .WithName("Retornar Produtos")
            .WithSummary("Retorna os Produtos");

        produtos.MapPost("/", InserirProdutos)
            .WithName("Inserir produtos")
            .WithSummary("Insere um novo produto na base de dados");

        produtos.MapPut("/{id}", AlterarProduto)
            .WithName("Alterar produto")
            .WithSummary("Altera um produto na base de dados")
            .RequireAuthorization();
    }

    private static async Task<IResult> InserirProdutos(RegistrarProdutoDTO produto, [FromServices]IProdutoUseCase produtoService)
    {
        try
        {
            var produtoDTO = new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                ImagemUrl = produto.ImagemUrl,
                DataCadastro = produto.DataCadastro,
                CategoriaId = produto.CategoriaId
            };
            var resultado = await produtoService.InserirProduto(produtoDTO);
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
            return TypedResults.Problem("Internal server error");
        }
    }

    private static async Task<IResult> RetornarProdutos([FromServices]IProdutoUseCase produtoService)
    {
        return TypedResults.Ok();
    }

    private static async Task<IResult> AlterarProduto(int id, RegistrarProdutoDTO produto, [FromServices]IProdutoUseCase produtoService)
    {
        return TypedResults.Ok();
    }
}
