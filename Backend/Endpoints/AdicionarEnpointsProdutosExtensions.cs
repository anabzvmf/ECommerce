using DTO.Produtos;
using Model.Produtos;
using UseCases;
using UseCases.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Identity.Data;

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


        // produtos.MapGet("/", RetornarProdutos)
        //     .WithName("Retornar Produtos")
        //     .WithSummary("Retorna todos os produtos");


        produtos.MapPost("/", InserirProdutos)
            .WithName("Inserir Produtos")
            .WithSummary("Insere um novo produto na base de dados");


        // produtos.MapPut("/{id}", AlterarProduto)
        //     .WithName("Alterar Produto")
        //     .WithSummary("Altera um produto na base de dados")
        //     .RequireAuthorization();
    }
    private static async Task<IResult> InserirProdutos(RegistrarProdutoDTO produto, [FromServices] IProdutoUseCase produtoService)
    {
        try
        {
            if (produto == null)
            {
                return TypedResults.BadRequest("Dados inválidos.");
            }

            var produtoDTO = new RegistrarProdutoDTO
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Estoque = produto.Estoque
            };

        
            var resultado = await produtoService.InserirProduto(produtoDTO);

            return resultado.Sucesso
                ? TypedResults.Created($"/produtos/{resultado.Objeto.Id}", resultado.Objeto)
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
}