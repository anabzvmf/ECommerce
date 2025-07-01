using Model.ControleAcessos;
using Model.Produtos;
using Infra.ControlesAcessos;
using UseCases.ControleAcessos;
using Mappers.ControleAcessos;
using Mappers.Produtos;
using Mappers;
using DTO.ControleAcessos;
using DTO.Produtos;

using Microsoft.AspNetCore.Identity;
using Infra.Produtos;
using UseCases.Produtos;

namespace Backend
{
    public static class InjecoesDependencias
    {
        internal static void InjetarDependencias(this IServiceCollection services)
        {
            // Carrinho dependencies
            services.AddScoped<UseCases.CarrinhoCompras.ICarrinhoService, UseCases.CarrinhoCompras.CarrinhoServiceUseCase>();
            services.AddScoped<Infra.CarrinhoCompras.ICarrinhoDAO, Infra.CarrinhoCompras.CarrinhoComprasDAO>();

            services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

            #region DAOs
            services.AddScoped<IUsuarioDAO, UsuarioDAO>();
            services.AddScoped<IProdutoDAO, ProdutosDAO>();
            // services.AddScoped<IPostagemDAO, PostagemDAO>();
            // services.AddScoped<IPostagemReacaoDAO, PostagemReacaoDAO>();
            #endregion

            #region UseCases
            services.AddScoped<IControleAcessoUseCase, ControleAcessoUseCase>();
            services.AddScoped<IProdutoUseCase, ProdutoUseCase>();
            // services.AddScoped<IPostagemUseCase, PostagemUseCase>();
            // services.AddScoped<IPostagemReacaoUseCase, PostagemReacaoUseCase>();
            #endregion

            #region Mappers
            services.AddScoped<IMapper<Usuario, UsuarioDTO>, UsuarioMapper>();
            services.AddScoped<IMapper<Produto, ProdutoDTO>, ProdutoMapper>();
            // services.AddScoped<IMapper<Postagem, PostagemDTO>, PostagemMapper>();
            // services.AddScoped<IMapper<PostagemReacao, PostagemReacaoDTO>, PostagemReacaoMapper>();
            #endregion
        }
    }
}