using DTO.ControleAcessos;
using Model.ControleAcessos;
using UseCases;
using UseCases.ControleAcessos;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Backend.Endpoints;

public static class AdicionarEnpointsUsuariosExtensions
{
    /// <summary>
    /// Método para adicionar os endpoints de usuários da API.
    /// </summary>
    /// <param name="app">Instância do WebApplication.</param>
    public static void AdicionarEndpointsUsuarios(this WebApplication app)
    {
        var usuarios = app.MapGroup("/usuarios")
            .WithTags("Usuários")
            .WithDescription("Endpoints relacionados a usuários");

        usuarios.MapGet("/", RetornarPrincipaisAutores)
            .WithName("Retornar Principais Autores")
            .WithSummary("Retorna os principais autores");

        usuarios.MapPost("/", InserirUsuario)
            .WithName("Inserir usuário")
            .WithSummary("Insere um novo usuário na base de dados");

        usuarios.MapPut("/{id}", AlterarUsuario)
            .WithName("Alterar usuário")
            .WithSummary("Altera um usuário na base de dados")
            .RequireAuthorization();

        usuarios.MapGet("/{id}", ObterUsuarioPorId)
            .WithName("Obter usuário por id")
            .WithSummary("Retorna o usuário que possui o id informado")
            .RequireAuthorization();

        usuarios.MapGet("/email/{email}", ObterUsuarioPorEmail)
            .WithName("Obter usuário por e-mail")
            .WithSummary("Retorna o usuário que possui o e-mail informado");

        usuarios.MapPost("/login", Login)
            .WithName("Login")
            .WithSummary("Permite o login de usuário");

        usuarios.MapPost("/alterar-senha", AlterarSenha)
            .WithName("Alterar Senha")
            .WithSummary("Permite alterar a senha de usuário");
    }

    private static async Task<IResult> InserirUsuario(RegistrarUsuarioDTO usuario, IControleAcessoUseCase controleAcessoUseCase)
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

    
    private static async Task<IResult> RetornarPrincipaisAutores(HttpContext context, IControleAcessoUseCase controleAcessoUseCase, IDistributedCache cache)
    {
        string cacheKey = $"{nameof(RetornarPrincipaisAutores)}";

        try
        {
            var objetosEmCache = await cache.GetAsync(cacheKey);

            if (objetosEmCache != null)
                return TypedResults.Content(Encoding.UTF8.GetString(objetosEmCache), "application/json");

            var resultado = await controleAcessoUseCase.ObterPrincipaisAutores();
            
            return resultado.Sucesso
                ? TypedResults.Ok(await cache.SetCacheAndReturnObjectAsync(cacheKey, resultado.Objetos))
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

    private static async Task<IResult> ObterUsuarioPorId(long id, HttpContext context, IControleAcessoUseCase controleAcessoUseCase)
    {
        try
        {
            controleAcessoUseCase.IdentificarAcesso(context.User.GetId());

            var resultado = await controleAcessoUseCase.ObterUsuarioPorId(id);
            return resultado.Sucesso
                ? TypedResults.Ok(resultado.Objeto)
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

    private static async Task<IResult> ObterUsuarioPorEmail(string email, HttpContext context, IControleAcessoUseCase controleAcessoUseCase)
    {
        try
        {
            var resultado = await controleAcessoUseCase.ObterUsuarioPorEmail(email);
            return resultado.Sucesso
                ? TypedResults.Ok(resultado.Objeto)
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
    
    private static async Task<IResult> AlterarUsuario(long id, UsuarioDTO usuario, HttpContext context, IControleAcessoUseCase controleAcessoUseCase)
    {
        try
        {
            controleAcessoUseCase.IdentificarAcesso(context.User.GetId());

            if (id != usuario.Id)
                return TypedResults.BadRequest("O id não confere.");

            var resultado = await controleAcessoUseCase.AlterarUsuario(usuario);
            return resultado.Sucesso
                ? TypedResults.Ok()
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

    private static async Task<IResult> Login(LoginDTO login, IControleAcessoUseCase controleAcessoUseCase)
    {
        try
        {
            Console.WriteLine($"[Login] Recebendo login para: {login.Email}");

            var resultado = await controleAcessoUseCase.LogarAsync(login);
            
            if (!resultado.Sucesso)
            {
                Console.WriteLine("[Login] Falha no login: credenciais inválidas");
                return TypedResults.Unauthorized();
            }

            var token = new TokenService().Gerar(resultado.Objeto);
            Console.WriteLine($"[Login] Login bem-sucedido para: {login.Email}, Token gerado.");

            return TypedResults.Ok(new TokenService().Gerar(resultado.Objeto));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Login] ERRO ao tentar login para {login.Email}:\n {ex.Message}");
            // Console.WriteLine(ex.StackTrace);
            return TypedResults.InternalServerError();
        }
    }
//         {
// #if DEBUG
//             var metodo = MethodBase.GetCurrentMethod();

        //             if (metodo != null)
        //                 Debug.WriteLine($"Exception in {metodo.Name}: {ex.Message}");
        // #endif

        //             return TypedResults.InternalServerError();
        //         }
        //     }

    private static async Task<IResult> AlterarSenha(TrocarSenhaDTO trocarSenha, IControleAcessoUseCase controleAcessoUseCase)
    {
        try
        {
            var resultado = await controleAcessoUseCase.TrocarSenhaAsync(trocarSenha);
            return resultado.Sucesso
                ? TypedResults.Ok()
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