using Model.Produtos;
using System;
using DTO.Carrinho;
using UseCases.CarrinhoCompras;
using Microsoft.AspNetCore.Routing;

namespace Backend.Endpoints;

public static class AdicionarEndpointsCarrinhoExtensions
{
    public static void AdicionarEndpointsCarrinho(this IEndpointRouteBuilder app)
    {

        app.MapPost("/carrinho/adicionar", async (CarrinhoItemDTO itemDto, ICarrinhoService service) =>
        {
            await service.AdicionarItemAsync(itemDto);
            return Results.Ok();
        });

        app.MapGet("/carrinho/{id}", async (long id, ICarrinhoService service) =>
        {
            var carrinho = await service.ObterCarrinhoAsync(id);
            return Results.Ok(carrinho);
        });
    }
}
