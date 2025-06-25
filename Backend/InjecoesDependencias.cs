using Model.ControleAcessos;
using Infra.ControlesAcessos;
using UseCases.ControleAcessos;
using Mappers.ControleAcessos;
using Mappers;
using DTO.ControleAcessos;

using Microsoft.AspNetCore.Identity;

namespace Backend
{
    public static class InjecoesDependencias
    {
        internal static void InjetarDependencias(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
            
            #region DAOs
            
            services.AddScoped<IUsuarioDAO, UsuarioDAO>();
            // services.AddScoped<IPostagemDAO, PostagemDAO>();
            // services.AddScoped<IPostagemReacaoDAO, PostagemReacaoDAO>();

            #endregion

            #region UseCases

            services.AddScoped<IControleAcessoUseCase, ControleAcessoUseCase>();
            // services.AddScoped<IPostagemUseCase, PostagemUseCase>();
            // services.AddScoped<IPostagemReacaoUseCase, PostagemReacaoUseCase>();

            #endregion

            #region Mappers

            services.AddScoped<IMapper<Usuario, UsuarioDTO>, UsuarioMapper>();
            // services.AddScoped<IMapper<Postagem, PostagemDTO>, PostagemMapper>();
            // services.AddScoped<IMapper<PostagemReacao, PostagemReacaoDTO>, PostagemReacaoMapper>();

            #endregion
        }
    }
}