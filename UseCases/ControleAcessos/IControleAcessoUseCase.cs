using DTO.ControleAcessos;

namespace UseCases.ControleAcessos;

public interface IControleAcessoUseCase
{
    void IdentificarAcesso(long idUsuario);

    #region Todos_Usuarios

    Task<ResultadoUnico<UsuarioDTO>> LogarAsync(LoginDTO login);

    Task<ResultadoVoid> TrocarSenhaAsync(TrocarSenhaDTO trocarSenha);

    Task<ResultadoUnico<UsuarioDTO>> InserirUsuario(UsuarioDTO usuario);

    #endregion

    #region Apenas_Logados

    Task<ResultadoVoid> AlterarUsuario(UsuarioDTO usuario);
    Task<ResultadoUnico<UsuarioDTO>> ObterUsuarioPorId(long id);
    Task<ResultadoUnico<UsuarioDTO>> ObterUsuarioPorEmail(string email);
    Task<ResultadoLista<UsuarioDTO>> ObterPrincipaisAutores();

    #endregion

    Task<ResultadoUnico<UsuarioDTO>> RegistrarUsuario(RegistrarUsuarioDTO usuario);
}