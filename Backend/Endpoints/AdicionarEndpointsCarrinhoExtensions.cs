using System;
using DTO.Carrinho;
using UseCases.Carrinho;
using Microsoft.AspNetCore.Routing;

namespace Backend.Endpoints;

public static class AdicionarEndpointsCarrinhoExtensions
{
    public static void AdicionarEndpointsCarrinho(this IEndpointRouteBuilder app)
    {
        app.MapPost("/carrinho/adicionar", async (CarrinhoDTO carrinho, ICarrinhoService service) =>
        {
            await service.AdicionarItemAsync(carrinho);
            return Results.Ok();
        });

        app.MapGet("/carrinho", async (ICarrinhoService service) =>
        {
            var carrinho = await service.ObterCarrinhoAsync();
            return Results.Ok(carrinho);
        });
    }
}
