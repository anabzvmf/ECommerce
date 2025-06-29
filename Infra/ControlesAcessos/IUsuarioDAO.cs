using Model.ControleAcessos;

namespace Infra.ControlesAcessos;

public interface IUsuarioDAO : IBaseDAO<Usuario>
{
    Task<Usuario?> RetornarPorEmailAsync(string email);
    Task<IEnumerable<Usuario>> RetornarPrincipaisAutoresAsync();
}